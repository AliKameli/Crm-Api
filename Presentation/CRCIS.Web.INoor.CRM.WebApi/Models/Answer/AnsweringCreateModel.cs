using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.WebApi.Models.Answer
{
    public class AnsweringCreateModel
    {
        public string AnswerText { get; set; }
        public int AnswerMethodId { get; set; }
        public long CaseId { get; set; }
        public int AdminId { get; set; }
    }
}