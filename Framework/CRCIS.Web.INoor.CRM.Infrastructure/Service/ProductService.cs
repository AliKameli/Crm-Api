using CRCIS.Web.INoor.CRM.Contract.Repositories.Sources;
using CRCIS.Web.INoor.CRM.Contract.Service;
using CRCIS.Web.INoor.CRM.Domain.Sources.Product.Commands;
using CRCIS.Web.INoor.CRM.Utility.Enums;
using CRCIS.Web.INoor.CRM.Utility.Enums.Extensions;
using CRCIS.Web.INoor.CRM.Utility.Extensions;
using CRCIS.Web.INoor.CRM.Utility.Response;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger _logger;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<DataResponse<int>> CreateAsync(ProductCreateCommand command)
        {
            var resposne = new DataResponse<int>(false);
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    await _productRepository.CreateAsync(command);
                    transaction.Complete();
                    resposne = new DataResponse<int>(true);
                }
                catch (Exception ex)
                {
                    _logger.LogException(ex);
                    resposne = new DataResponse<int>(false);
                    resposne.AddError("خطایی در ثبت محصول رخ داده است");
                }
            }

            return resposne;
        }


        public async Task<DataResponse<int>> CreateFromNoorlock(ProductBySecretCreateCommand command)
        {
            var resposne = new DataResponse<int>(false);

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var commandInsert = new ProductCreateCommand(command.Title, ProductType.Descktop.ToInt32(), command.Code);
                    var insertResonse = await _productRepository.CreateAsync(commandInsert);
                    if (insertResonse.Success == false)
                    {
                        return insertResonse;
                    }
                    var commandUpdateSecret = new ProductUpdateSecretCommand
                    {
                        Id = insertResonse.Data,
                        Secret = command.Secret,
                    };

                    var reponseUpdateSecret = await _productRepository.UpdateSecretAsync(commandUpdateSecret);

                    transaction.Complete();
                    resposne = new DataResponse<int>(true);

                }
                catch (Exception ex)
                {
                    _logger.LogException(ex);
                    resposne = new DataResponse<int>(false);
                    resposne.AddError("خطایی در ثبت محصول از نورلاک رخ داده است");
                }
            }

            return resposne;
        }

        public async Task<DataResponse<int>> UpdateAsync(ProductUpdateCommand command)
        {
            var resposne = new DataResponse<int>(false);
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    await _productRepository.UpdateAsync(command);
                    transaction.Complete();
                    resposne = new DataResponse<int>(true);
                }
                catch (Exception ex)
                {
                    _logger.LogException(ex);
                    resposne = new DataResponse<int>(false);
                    resposne.AddError("خطایی در بروزرسانی محصول رخ داده است");
                }
            }

            return resposne;
        }
    }
}