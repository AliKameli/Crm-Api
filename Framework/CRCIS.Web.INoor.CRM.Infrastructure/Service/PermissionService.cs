using CRCIS.Web.INoor.CRM.Contract.Repositories.Permissions.RoleAdmin;
using CRCIS.Web.INoor.CRM.Contract.Service;
using CRCIS.Web.INoor.CRM.Domain.Permissions.AdminAction.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Permissions.Dtos;
using CRCIS.Web.INoor.CRM.Utility.Extensions;
using CRCIS.Web.INoor.CRM.Utility.Response;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Service
{
    public class PermissionService : IPermissionService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IRoleAdminRepository _roleAdminRepository;
        private readonly ILogger _logger;
        public PermissionService(IRoleAdminRepository roleAdminRepository, ILoggerFactory loggerFactory, IMemoryCache memoryCache)
        {
            _roleAdminRepository = roleAdminRepository;
            _memoryCache = memoryCache;
            _logger = loggerFactory.CreateLogger<PermissionService>();
        }

        public async Task<DataTableResponse<IEnumerable<PermissionDto>>> GetPermissionsByAdminIdAsync(int adminId)
        {
            string casheKey = $"PermissionService_GetPermissionsByAdminIdAsync_{ adminId}";

            IEnumerable<PermissionDto> permissions = null;
            //try
            //{
            //    if (_memoryCache.TryGetValue<IEnumerable<PermissionDto>>(casheKey, out permissions))
            //    {
            //        if (permissions is not null)
            //            return new DataTableResponse<IEnumerable<PermissionDto>>(permissions, permissions.Count());
            //    }
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogException(ex); ;
            //}
            var response = await _roleAdminRepository.GetPermissionActionListByAdminId(adminId);
            if (response.Success ==false)
            {
                return new DataTableResponse<IEnumerable<PermissionDto>>(false);
            }
            if (response.Data is null || response.Data.Any() == false)
            {
                permissions = new List<PermissionDto>();
                return new DataTableResponse<IEnumerable<PermissionDto>>(permissions, 0);
            }
            permissions = response.Data.Select(action => new PermissionDto { ActionId = action, AdminId = adminId });

            // To store a value
            _memoryCache.Set<IEnumerable<PermissionDto>>(casheKey, permissions);


            return new DataTableResponse<IEnumerable<PermissionDto>>(permissions,permissions.Count());
        }
    }
}
