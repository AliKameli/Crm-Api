﻿using CRCIS.Web.INoor.CRM.Domain.Sources.SourceConfig;
using CRCIS.Web.INoor.CRM.Utility.Response;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Repositories.Sources
{
    public interface ISourceConfigRepository
    {
        Task<DataResponse<SourceConfigModel>> GetByIdAsync(int id);
    }
}
