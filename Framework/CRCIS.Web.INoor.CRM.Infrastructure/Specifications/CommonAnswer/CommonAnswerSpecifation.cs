using CRCIS.Web.INoor.CRM.Domain.Answers.CommonAnswer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Specifications.CommonAnswer
{
    public static class CommonAnswerSpecifation
    {
        public static CommonAnswerGetFullDto PairCommandAccess(this CommonAnswerGetFullDto dto, int currentAdminId)
        {
            if (dto.ConfirmedAdminId is null)//تایید نشده
            {
                dto.AccessOperatorBeforConfirmAdmin = dto.CreatorAdminId == currentAdminId;
                return dto;
            }


            return dto;
        }
    }
}
