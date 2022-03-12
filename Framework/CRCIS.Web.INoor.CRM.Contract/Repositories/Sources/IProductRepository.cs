﻿using CRCIS.Web.INoor.CRM.Domain.Sources.Product;
using CRCIS.Web.INoor.CRM.Domain.Sources.Product.Commands;
using CRCIS.Web.INoor.CRM.Domain.Sources.Product.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Sources.Product.Queries;
using CRCIS.Web.INoor.CRM.Utility.Dtos;
using CRCIS.Web.INoor.CRM.Utility.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Repositories.Sources
{
    public interface IProductRepository
    {
        Task<DataResponse<int>> CreateAsync(ProductCreateCommand command);
        Task<DataResponse<int>> DeleteAsync(int id);
        Task<DataTableResponse<IEnumerable<ProductGetDto>>> GetAsync(ProductDataTableQuery query);
        Task<DataResponse<ProductModel>> GetByIdAsync(int id);
        Task<DataResponse<ProductModel>> GetBySecretKeyAsync(string secretKey);
        Task<DataResponse<IEnumerable<ProductDropDownListDto>>> GetDropDownListAsync(bool appendCode = true);
        Task<DataResponse<int>> UpdateAsync(ProductUpdateCommand command);
        Task<DataResponse<int>> UpdateSecretAsync(ProductUpdateSecretCommand command);
    }
}
