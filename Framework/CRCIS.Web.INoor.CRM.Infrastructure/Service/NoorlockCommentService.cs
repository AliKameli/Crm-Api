using CRCIS.Web.INoor.CRM.Contract.Repositories.Reports;
using CRCIS.Web.INoor.CRM.Contract.Security;
using CRCIS.Web.INoor.CRM.Contract.Service;
using CRCIS.Web.INoor.CRM.Domain.Reports.NoorLock.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Reports.NoorLock.Queries;
using CRCIS.Web.INoor.CRM.Utility.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Service
{
    public class NoorlockCommentService : INoorlockCommentService
    {
        private readonly ISecurityService _securityService;
        private readonly IReportRepository _reportRepository;
        public NoorlockCommentService(ISecurityService securityService, IReportRepository reportRepository)
        {
            _securityService = securityService;
            _reportRepository = reportRepository;
        }

        public  Task<DataTableResponse<IEnumerable<NoorLockCaseReportDto>>> GetNoorAppReportPaging(
            int pageSize,int pageIndex,
           Guid inoorId,
           string productSecret = null)
        {;
            var query = new NoorLockReportPagingQuery(inoorId, productSecret, pageIndex, pageSize);

            return _reportRepository.GetNoorAppPagingReport(query);
        }

        public  Task<DataResponse<NoorLockCaseReportDto>> GetNoorAppGetByCaseId(long caseId, Guid noorUserId, string productSecret)
        {
            var query = new NoorAppReportCaseIdQuery(caseId,noorUserId,productSecret);
            return _reportRepository.GetNoorAppReportByCaseId(query);
        }
        public Task<DataTableResponse<IEnumerable<NoorLockCaseReportDto>>> GetNoorlockPaging(int pageSize,
           int pageIndex,
           bool? typeOfComment = null,
           long? snId = null,
           string sk = null,
           string activationCode = null,
           string productSecret = null)
        {
            var noorlockKey = new NoorLockAppKeyDto
            {
                NoorLockTypeOfComment = typeOfComment,
                NoorLockSnId = snId,
                NoorLockSk = sk,
                NoorLockActivationCode = activationCode,
            };
            var json = System.Text.Json.JsonSerializer.Serialize(noorlockKey);
            var apphashKey = _securityService.GetSha256HashHex(json);
            var query = new NoorLockReportPagingQuery(apphashKey, productSecret, pageIndex, pageSize);

            return _reportRepository.GetNoorLockPagingReportAsync(query);
        }


        public Task<DataResponse<NoorLockCaseReportDto>> GetNoorlockGetByRowNumber(
           long rowNumber,
           Guid? inoorId = null,
           bool? typeOfComment = null,
           long? snId = null,
           string sk = null,
           string activationCode = null,
           string productSecret = null)
        {
            var noorlockKey = new NoorLockAppKeyDto
            {
                NoorLockTypeOfComment = typeOfComment,
                NoorLockSnId = snId,
                NoorLockSk = sk,
                NoorLockActivationCode = activationCode,
            };
            var json = System.Text.Json.JsonSerializer.Serialize(noorlockKey);
            var apphashKey = _securityService.GetSha256HashHex(json);
            var query = new NoorLockReportRowNumberQuery(inoorId, apphashKey, productSecret, rowNumber);

            return _reportRepository.GetNoorLockReportByRowNumberAsync(query);
        }


    }
}
