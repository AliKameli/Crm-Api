using AutoMapper;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Cases;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Sources;
using CRCIS.Web.INoor.CRM.Contract.Security;
using CRCIS.Web.INoor.CRM.Contract.Settings;
using CRCIS.Web.INoor.CRM.Domain.Cases.RabbitImport.Commands;
using CRCIS.Web.INoor.CRM.Domain.Cases.RabbitImport.Dtos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.RabbitMq
{
    public class ConsumerRabbitMQHostedService : BackgroundService
    {
        private readonly ILogger _logger;

        private readonly ISecurityService _securityService;
        private readonly IServiceProvider _serviceProvider;
        private readonly IRabbitmqSettings _rabbitmqSettings;
        private IConnection _connection;
        private IModel _channel;
        public ConsumerRabbitMQHostedService(ILoggerFactory loggerFactory, IServiceProvider serviceProvider,
            ISecurityService securityService, IRabbitmqSettings rabbitmqSettings)
        {

            _securityService = securityService;
            _rabbitmqSettings = rabbitmqSettings;
            _logger = loggerFactory.CreateLogger<ConsumerRabbitMQHostedService>();
            _serviceProvider = serviceProvider;
            InitRabbitMQ();
        }

        private void InitRabbitMQ()
        {
            var factory = new ConnectionFactory
            {
                HostName = _rabbitmqSettings.HostName,//"webrabbit.pars.local",
                VirtualHost =_rabbitmqSettings.VirtualHost,// "crm",
                UserName =_rabbitmqSettings.Username ,//"crm",
                Password = _rabbitmqSettings.Password,//"jxeF5#e7Fp",
                Port = 5672
            };

            // create connection  
            _connection = factory.CreateConnection();

            Dictionary<string, object> config = new Dictionary<string, object>();
            config.Add("x-queue-mode", "lazy");
            // create channel  
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(_rabbitmqSettings.ExchangeFeedback/*"main"*/, ExchangeType.Fanout, true, false);
            _channel.QueueDeclare(_rabbitmqSettings.QueueFeedback/*"user-report-support"*/, true, false, false, null);
            _channel.QueueDeclare("logs", true, false, false, config);
            _channel.QueueBind(_rabbitmqSettings.QueueFeedback/*"user-report-support"*/, _rabbitmqSettings.ExchangeFeedback/*"main"*/, "", null);
            _channel.QueueBind("logs", _rabbitmqSettings.ExchangeFeedback/*"main"*/, "", null);
            _channel.BasicQos(0, 1, false);

            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                // received message  
                var content = System.Text.Encoding.UTF8.GetString(ea.Body.ToArray());

                // handle the received message  
                HandleMessage(content);
                //channel.BasicAck(args.DeliveryTag, false);
                _channel.BasicAck(ea.DeliveryTag, false);

            };
            //consumer.Shutdown += OnConsumerShutdown;
            //consumer.Registered += OnConsumerRegistered;
            //consumer.Unregistered += OnConsumerUnregistered;
            //consumer.ConsumerCancelled += OnConsumerConsumerCancelled;

            _channel.BasicConsume(_rabbitmqSettings.QueueFeedback/*"user-report-support"*/, false, consumer);
            return Task.CompletedTask;

        }
        private void HandleMessage(string content)
        {
            try
            {
                _logger.LogInformation($"crm received {content}");

                var dto = System.Text.Json.JsonSerializer.Deserialize<RabbitImportCaseCreateDto>(content);

                if (dto == null)
                {
                    _logger.LogWarning($"json convert not worked: {content}");
                    return;
                }
                if (string.IsNullOrEmpty(dto?.Client?.ClientSecret))
                {
                    _logger.LogWarning($"client in json not founded: {content}");
                    return;
                }

                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    IProductRepository _productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();
                    var productResponse = _productRepository.GetBySecretKeyAsync(dto?.Client?.ClientSecret).Result;
                    Guid? noorUserId = null;
                    if (!string.IsNullOrEmpty(dto.User.NoorUserId))
                    {
                        Guid.TryParse(dto.User.NoorUserId, out Guid tempNoorUserId);
                        noorUserId = tempNoorUserId;
                    }
                    if (productResponse.Success == false)
                    {
                        _logger.LogWarning($"product secret not found : {dto?.Client?.ClientSecret} ,json : {content}");
                        return;
                    }
                    var productId = productResponse.Data.Id;
                    var mobile = string.IsNullOrEmpty(dto?.MessageInfo?.Mobile) ? "" : dto?.MessageInfo?.Mobile;

                    var appKeyHash = dto?.AppKey == null ?
                        null :
                        _securityService.GetSha256HashHex(System.Text.Json.JsonSerializer.Serialize(dto.AppKey));

                    var command = new RabbitImportCaseCreateCommand(dto.MessageInfo.Title,
                        dto.MessageInfo?.NameFamily,
                        dto.MessageInfo?.Email,
                        dto.MessageInfo?.Description,
                        1,
                        noorUserId,
                        productId,
                        null,
                        mobile,
                        dto.MessageInfo?.CreateDateTime,
                        dto.Client?.PageTitle,
                        dto.Client?.PageUrl,
                        dto.Client?.ToMailBox,
                        dto.MessageInfo?.FileUrl,
                        dto.MessageInfo?.FileType,
                        dto.User?.UserLanguage,
                        dto.User?.Ip,
                        dto.Device?.Browser,
                        dto.Device?.UserAgent,
                        dto.Device?.Platform,
                        dto.Device?.Os,
                        dto.Device?.DeviceScreenSize,
                        appKeyHash,
                        dto?.AppData?.NoorLockSk,
                        dto?.AppData?.NoorLockSnId,
                        dto?.AppData?.NoorLockActivationCode,
                        dto?.AppData?.NoorLockTypeOfComment
                          );


                    using (IServiceScope scope2 = _serviceProvider.CreateScope())
                    {
                        IRabbitImportCaseRepository _importCaseRepository = scope2.ServiceProvider.GetRequiredService<IRabbitImportCaseRepository>();
                        var insertResponse = _importCaseRepository.CreateFromRabbiImportAsync(command).Result;

                        if (insertResponse.Success == false)
                            _logger.LogError($"rabbit insert not successful : {content}");
                    }

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
            }
        }

        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e) { }
        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerRegistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerShutdown(object sender, ShutdownEventArgs e) { }
        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e) { }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
