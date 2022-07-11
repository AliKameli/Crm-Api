using CRCIS.Web.INoor.CRM.Contract.Repositories.Alarms.WarningHistory;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Alarms.Warnings;
using CRCIS.Web.INoor.CRM.Contract.Service;
using CRCIS.Web.INoor.CRM.Domain.Alarms.WarningHistory.Commands;
using CRCIS.Web.INoor.CRM.Domain.Alarms.Warnings.Commands;
using CRCIS.Web.INoor.CRM.Domain.Alarms.Warnings.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Alarms.Warnings.Queries;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication.Extensions;
using CRCIS.Web.INoor.CRM.Utility.Enums;
using CRCIS.Web.INoor.CRM.Utility.Enums.Extensions;
using CRCIS.Web.INoor.CRM.Utility.Extensions;
using CRCIS.Web.INoor.CRM.Utility.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Service
{
    public class WarningService : IWarningService
    {
        private ILogger _logger;
        private readonly IIdentity _identity;
        private readonly IWarningRepository _warningRepository;
        private readonly IWarningHistoryRepository _warningHistoryRepository;
        private readonly IConfiguration _configuration;
        public WarningService(IIdentity identity,
            IWarningRepository warningRepository,
            IWarningHistoryRepository warningHistoryRepository, IConfiguration configuration)
        {
            _identity = identity;
            _warningRepository = warningRepository;
            _warningHistoryRepository = warningHistoryRepository;
            _configuration = configuration;
        }

        public async Task<DataResponse<long>> CreateAsync(WarningCreateCommand command)
        {
            var response = await _warningRepository.CreateAsync(command);
            if (response.Success)
            {
                try
                {
                    int? adminId = _identity.IsAuthenticated ? _identity.GetAdminId() : null;
                    long warningId = response.Data;
                    var commandLog = new WarningHistoryCreateCommand(warningId, adminId, WarningHistoryType.CreateCommonAnswer);
                    await _warningHistoryRepository.CreateAsync(commandLog);
                }
                catch (Exception ex)
                {
                    _logger.LogException(ex);
                }
            }
            return response;

        }
        public async Task<DataResponse<long>> UpdateWarningAsVisitedAsync(long warningId)
        {
            var command = new WarningUpdateAsVistedCommand(warningId, _identity.GetAdminId());
            return await _warningRepository.UpdateWarningAsVisitedAsync(command);
        }
        public async Task<DataTableResponse<IEnumerable<WarningGetDto>>> GetWarningsAsync(WarningDataTableQuery query)
        {
            var resposnse = await _warningRepository.GetAsync(query);

            if (resposnse.Success)
            {

            }

            return resposnse;
        }

        public async Task<DataResponse<long>> GetImportantWarningsDayCountAsync()
        {
            var showLogTypes = _configuration.GetSection("ShowLogTypes").Get<List<string>>();
            var showLogTypeIds = string.Join(',', showLogTypes.Select(a => Enum.Parse<WarningHistoryType>(a)).Select(a => a.ToInt32()));
            var query = new ImportantWarningsDayQuery(System.DateTime.Today, showLogTypeIds);
            var response = await _warningRepository.GetImportantWarningsDayAsync(query);
            if (response.Success)
            {
                return new DataResponse<long>(response.TotalCount);
            }
            return new DataResponse<long>(0);
        }
    }
}