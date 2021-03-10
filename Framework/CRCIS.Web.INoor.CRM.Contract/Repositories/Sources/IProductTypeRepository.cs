using CRCIS.Web.INoor.CRM.Domain.Sources.Product.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Sources.ProductType;
using CRCIS.Web.INoor.CRM.Domain.Sources.ProductType.Commands;
using CRCIS.Web.INoor.CRM.Domain.Sources.ProductType.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Sources.ProductType.Queries;
using CRCIS.Web.INoor.CRM.Utility.Dtos;
using CRCIS.Web.INoor.CRM.Utility.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Repositories.Sources
{
    public interface IProductTypeRepository : IBaseRepository
    {
        Task<DataResponse<int>> CreateAsync(ProductTypeCreateCommand command);
        Task<DataResponse<int>> DeleteAsync(int id);
        Task<DataResponse<IEnumerable<ProductTypeGetDto>>> GetAsync(ProductTypeDataTableQuery query);
        Task<DataResponse<ProductTypeModel>> GetByIdAsync(int id);
        Task<DataResponse<IEnumerable<DropDownListDto>>> GetDropDownListAsync();
        Task<DataResponse<int>> UpdateAsync(ProductTypeUpdateCommand command);
    }
}
