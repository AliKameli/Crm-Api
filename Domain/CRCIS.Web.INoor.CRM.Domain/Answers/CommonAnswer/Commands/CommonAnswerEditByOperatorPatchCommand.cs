using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Answers.CommonAnswer.Commands
{
   public class CommonAnswerEditByOperatorPatchCommand
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AnswerText { get; set; }
    }
}