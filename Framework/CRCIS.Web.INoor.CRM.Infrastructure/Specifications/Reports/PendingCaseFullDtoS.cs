using CRCIS.Web.INoor.CRM.Domain.Reports.Person.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Specifications.Reports
{
    public static class PendingCaseFullDtoS
    {
        public static PersonReportResponseFullDto PairCommandAccess(this PersonReportResponseFullDto dto, int currentAdminId)
        {
            if (dto == null)
                return null;

            dto.AllowAssignToMe = (dto.TblNumber == 1);
            dto.AllowAnswerByMe = (dto.TblNumber == 2 && dto.AdminId != null && dto.AdminId == currentAdminId);
            dto.AllowAssignToOther = (dto.TblNumber == 2 && dto.AdminId != null && dto.AdminId == currentAdminId);
            dto.AllowBackFromArchiveToMe = (dto.TblNumber == 3);

            return dto;
        }
    }
}
