using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Answers.CommonAnswer.Commands
{
    public class CommonAnswerCreateCommand
    {
        public string Title { get;private set; }
        public string AnswerText { get; private set; }
        public int Priority { get; private set; }

        public CommonAnswerCreateCommand(string title,string answerText, int priority)
        {
            Title = title;
            AnswerText = answerText;
            Priority = priority;
        }
    }
}
