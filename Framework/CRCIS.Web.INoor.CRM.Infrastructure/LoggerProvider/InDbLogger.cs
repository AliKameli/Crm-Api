using CRCIS.Web.INoor.CRM.Domain.Logs.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;


namespace CRCIS.Web.INoor.CRM.Infrastructure.LoggerProvider
{
    public class InDbLogger : ILogger
    {
        private readonly string _loggerName;
        private readonly InDbLoggerProvider _loggerProvider;
        private readonly LogLevel _minLevel;
        private readonly IServiceProvider _serviceProvider;

        public InDbLogger(InDbLoggerProvider loggerProvider,
            IServiceProvider serviceProvider,
            string loggerName,
            LogLevel minLevel)
        {
            _loggerName = loggerName;
            _serviceProvider = serviceProvider;
            _loggerProvider = loggerProvider;
            _minLevel = minLevel;
        }

        private static void SetStateJson<TState>(TState state, LogCreateCommand log)
        {
            try
            {
                log.MoreData = System.Text.Json.JsonSerializer.Serialize(state);
            }
            catch
            {
                // don't throw exceptions from logger
            }
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return new NoopDisposable();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel >= _minLevel;
        }

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            var message = formatter(state, exception);

            if (exception != null)
            {
                message = $"{message}{Environment.NewLine}{exception}";
            }

            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            var httpContextAccessor = _serviceProvider.GetRequiredService<IHttpContextAccessor>();
            var log = new LogCreateCommand
            {
                Url = httpContextAccessor?.HttpContext != null ? httpContextAccessor.HttpContext.Request.Path.ToString() : string.Empty,
                EventId = eventId.Id,
                LogLevel = logLevel.ToString(),
                LoggerName = _loggerName,
                Message = message,
                CategoryName = "",
                
            };
            SetStateJson(state, log);
            _loggerProvider.AddLog(log);
        }

        private class NoopDisposable : IDisposable
        {
            public void Dispose()
            {
            }
        }
    }
}
