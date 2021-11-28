using CRCIS.Web.INoor.CRM.Domain.Reports.NoorLock.Dtos;
using CRCIS.Web.INoor.CRM.Utility.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Service
{
    public interface INoorlockCommentService
    {
        Task<DataResponse<NoorLockCaseReportDto>> GetNoorlockGetByRowNumber(long rowNumber, Guid? inoorId = null, string typeOfComment = null, long? snId = null, string sk = null, string activationCode = null, string productSecret = null);
        Task<DataTableResponse<IEnumerable<NoorLockCaseReportDto>>> GetNoorlockPaging(int pageSize, int pageIndex, string typeOfComment = null, long? snId = null, string sk = null, string activationCode = null, string productSecret = null);
        Task<DataTableResponse<IEnumerable<NoorLockCaseReportDto>>> GetNoorAppReportPaging(int pageSize, int pageIndex, Guid inoorId, string productSecret = null);
        Task<DataResponse<NoorLockCaseReportDto>> GetNoorAppGetByCaseId(long caseId);
    }
}
