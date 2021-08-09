using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.WebApi.Models.CommonAnswer
{
    public class CommonAnswerCreateModel
    {
        public string Title { get; set; }
        public string AnswerText { get; set; }
        public int Priority { get; set; }
    }
}
