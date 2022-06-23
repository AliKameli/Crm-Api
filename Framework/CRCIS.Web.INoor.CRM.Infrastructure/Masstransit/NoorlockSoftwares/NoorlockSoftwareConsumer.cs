using CRCIS.Web.INoor.CRM.Contract.Repositories.Sources;
using CRCIS.Web.INoor.CRM.Contract.Service;
using CRCIS.Web.INoor.CRM.Domain.Sources.Product.Commands;
using CRCIS.Web.INoor.CRM.Utility.Extensions;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Masstransit.NoorlockSoftwares
{
    public class NoorlockSoftwareConsumer : IConsumer<NoorlockSoftwareInserted>
    {
        private readonly IProductService _productService;
        //private readonly ILogger _logger;
        public NoorlockSoftwareConsumer(IProductService productService/*, ILogger logger*/)
        {
            _productService = productService;
            //_logger = logger;
        }

        public async Task Consume(ConsumeContext<NoorlockSoftwareInserted> context)
        {
            //if (string.IsNullOrEmpty(context.Message.JsonString) == false)
            //{
                try
                {
                    var model = context.Message;
                    //var model = System.Text.Json.JsonSerializer.Deserialize<NoorlockSoftwareInsertedModel>(context.Message.JsonString);
                    var command = new ProductBySecretCreateCommand
                    {
                        Title = model.SotwareName,
                        Secret = model.CrmSoftwareSecret,
                        Code = model.NoorlockSoftwareCode,
                    };
                    var response = await _productService.CreateFromNoorlock(command);
                }
                catch (Exception ex)
                {
                    //_logger.LogException(ex);
                    throw ex;
                }
            //}
        }
    }
}
