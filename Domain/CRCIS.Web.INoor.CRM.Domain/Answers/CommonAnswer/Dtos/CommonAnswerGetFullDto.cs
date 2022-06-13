using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Answers.CommonAnswer.Dtos
{
    public class CommonAnswerGetFullDto : CommonAnswerGetDto
    {
        public bool AccessOperatorBeforConfirmAdmin { get; set; }
    }
}
