using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Answers.CommonAnswer.Commands
{
    public class CommonAnswerCreateCommand
    {
        public string AnswerText { get; private set; }
        public int Priority { get; private set; }

        public CommonAnswerCreateCommand(string answerText, int priority)
        {
            AnswerText = answerText;
            Priority = priority;
        }
    }
}
