using CRCIS.Web.INoor.CRM.Domain.Sources.Product.Commands;
using CRCIS.Web.INoor.CRM.Utility.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Service
{
    public interface IProductService
    {
        Task<DataResponse<int>> CreateAsync(ProductCreateCommand command);
        Task<DataResponse<int>> CreateFromNoorlock(ProductBySecretCreateCommand command);
        Task<DataResponse<int>> UpdateAsync(ProductUpdateCommand command);
    }
}
