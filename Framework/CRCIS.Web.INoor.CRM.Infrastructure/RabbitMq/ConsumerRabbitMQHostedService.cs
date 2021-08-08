using AutoMapper;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Cases;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Sources;
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
        private readonly IServiceProvider _serviceProvider;
        private IConnection _connection;
        private IModel _channel;
        public ConsumerRabbitMQHostedService(ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            _logger = loggerFactory.CreateLogger<ConsumerRabbitMQHostedService>();
            _serviceProvider = serviceProvider;
            InitRabbitMQ();
        }

        private void InitRabbitMQ()
        {
            var factory = new ConnectionFactory
            {
                HostName = "172.16.25.125",
                VirtualHost = "crm",
                UserName = "crm",
                Password = "jxeF5#e7Fp",
                Port = 5672
            };
            // create connection  
            _connection = factory.CreateConnection();

            // create channel  
            _channel = _connection.CreateModel();

            //_channel.ExchangeDeclare("demo.exchange", ExchangeType.Topic);
            //_channel.QueueDeclare("v", false, false, false, null);
            //_channel.QueueBind("user-report-support", "", "user-report-support", null);
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
                _channel.BasicAck(ea.DeliveryTag, false);

            };
            //consumer.Shutdown += OnConsumerShutdown;
            //consumer.Registered += OnConsumerRegistered;
            //consumer.Unregistered += OnConsumerUnregistered;
            //consumer.ConsumerCancelled += OnConsumerConsumerCancelled;

            _channel.BasicConsume("user-report-support", false, consumer);/////////////////////////////////////////////////////////////////
            return Task.CompletedTask;

        }
        private void HandleMessage(string content)
        {
            // we just print this message   
            _logger.LogInformation($"crm received {content}");

            var dto = System.Text.Json.JsonSerializer.Deserialize<RabbitImportCaseCreateDto>(content);
            
            if (dto == null)
            {
            }
            if (string.IsNullOrEmpty(dto.Client.ClientSecret))
            {
            }

            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                IProductRepository _productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();
                var productResponse = _productRepository.GetBySecretKeyAsync(dto.Client.ClientSecret).Result;
                Guid? noorUserId = null;
                if (!string.IsNullOrEmpty(dto.User.NoorUserId))
                {
                    Guid.TryParse(dto.User.NoorUserId, out Guid tempNoorUserId);
                    noorUserId = tempNoorUserId;
                }
                if (productResponse.Success)
                {
                    var productId = productResponse.Data.Id;
                    var command = new RabbitImportCaseCreateCommand(dto.MessageInfo.Title,
                        dto.MessageInfo.NameFamily,
                        dto.MessageInfo.Email,
                        dto.MessageInfo.Description,
                        1,
                        noorUserId,
                        productId,
                        null,
                        "",
                        dto.MessageInfo.CreateDateTime,
                        dto.Client.PageTitle,
                        dto.Client.PageUrl,
                        dto.Client.ToMailBox,
                        dto.MessageInfo.FileUrl,
                        dto.MessageInfo.FileType,
                        dto.User.UserLanguage,
                        dto.User.Ip,
                        dto.Device.Browser,
                        dto.Device.UserAgent,
                        dto.Device.Platform,
                        dto.Device.Os,
                        dto.Device.DeviceScreenSize
                          );

                    var t = command;

                    using (IServiceScope scope2 = _serviceProvider.CreateScope())
                    {
                        IRabbitImportCaseRepository _importCaseRepository = scope2.ServiceProvider.GetRequiredService<IRabbitImportCaseRepository>();
                        var insertResponse = _importCaseRepository.CreateFromRabbiImporttAsync(command).Result;
                    }
                }
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
