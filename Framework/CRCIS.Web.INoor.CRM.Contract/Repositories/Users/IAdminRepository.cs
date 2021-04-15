using CRCIS.Web.INoor.CRM.Domain.Users.Admin;
using CRCIS.Web.INoor.CRM.Domain.Users.Admin.Commands;
using CRCIS.Web.INoor.CRM.Domain.Users.Admin.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Users.Admin.Queries;
using CRCIS.Web.INoor.CRM.Utility.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Repositories.Users
{
    public interface IAdminRepository : IBaseRepository
    {
        Task<DataResponse<int>> CreateAsync(AdminCreateCommand command);
        Task<DataResponse<AdminModel>> FindAdminAsync(int adminId);
        Task<DataResponse<AdminModel>> FindAdminAsync(AdminLoginQuery query);
        Task<DataTableResponse<IEnumerable<AdminGetDto>>> GetAsync(AdminDataTableQuery query);
        Task<DataResponse<AdminModel>> GetByIdAsync(int id);
        Task<DataResponse<IEnumerable<AdminDropDownListDto>>> GetDropDownListAsync();
        Task<DataResponse<AdminModel>> GetProfileByIdAsync(int id);
        Task UpdateAdminLastActivityDateAsync(int adminId);
        Task<DataResponse<int>> UpdateAsync(AdminUpdateCommand command);
        Task<DataResponse<int>> UpdatePasswordHashAsync(int adminId, string newPasswordHash);
    }
}
