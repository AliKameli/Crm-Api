using CRCIS.Web.INoor.CRM.Contract.Repositories.Logs;
using CRCIS.Web.INoor.CRM.Domain.Cases.CaseStatus.Commands;
using CRCIS.Web.INoor.CRM.Domain.Logs.Commands;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.LoggerProvider
{
    public class InDbLoggerProvider : ILoggerProvider
    {

        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private readonly IList<LogCreateCommand> _currentBatch = new List<LogCreateCommand>();
        private readonly TimeSpan _interval = TimeSpan.FromSeconds(2);

        private readonly LogLevel _logLevel;

        private readonly BlockingCollection<LogCreateCommand> _messageQueue =
                new BlockingCollection<LogCreateCommand>(new ConcurrentQueue<LogCreateCommand>());

        private readonly Task _outputTask;
        private readonly IServiceProvider _serviceProvider;
        public InDbLoggerProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _serviceProvider = serviceProvider;

            _outputTask = Task.Run(ProcessLogQueue);
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new InDbLogger(this, _serviceProvider, categoryName, _logLevel);
        }


        private async Task ProcessLogQueue()
        {
            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                while (_messageQueue.TryTake(out var message))
                {
                    try
                    {
                        _currentBatch.Add(message);
                    }
                    catch
                    {
                        //cancellation token canceled or CompleteAdding called
                    }
                }

                await SaveLogsAsync(_currentBatch, _cancellationTokenSource.Token);
                _currentBatch.Clear();

                await Task.Delay(_interval, _cancellationTokenSource.Token);
            }
        }

        private async Task SaveLogsAsync(IList<LogCreateCommand> logs, CancellationToken cancellationToken)
        {
            try
            {
                if (!logs.Any())
                {
                    return;
                }

                // We need a separate context for the logger to call its SaveChanges several times,
                // without using the current request's context and changing its internal state.
                using (var scope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    using (var logRepository = scope.ServiceProvider.GetRequiredService<ILogRepository>())
                    {
                        await logRepository.CreateRangeAsync(logs, cancellationToken);
                    }
                }
            }
            catch
            {
                // don't throw exceptions from logger
            }
        }

        internal void AddLog(LogCreateCommand log)
        {
            if (!_messageQueue.IsAddingCompleted)
            {
                _messageQueue.Add(log, _cancellationTokenSource.Token);
            }
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
            _messageQueue.CompleteAdding();

            try
            {
                _outputTask.Wait(_interval);
            }
            catch (TaskCanceledException)
            {
            }
            catch (AggregateException ex) when (ex.InnerExceptions.Count == 1 &&
                                                ex.InnerExceptions[0] is TaskCanceledException)
            {
            }
        }
    }
}
