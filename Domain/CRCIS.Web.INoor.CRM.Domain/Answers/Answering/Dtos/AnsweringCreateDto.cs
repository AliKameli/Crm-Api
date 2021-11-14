using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Answers.Answering.Dtos
{
    public class AnsweringCreateDto
    {
        public string AnswerText { get;private set; }
        public string AnswerSource { get;private set; }
        public int AnswerMethodId { get;private set; }
        public long CaseId { get;private set; }
        public int AdminId { get;private set; }
    }
}
