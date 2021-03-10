using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Answers.CommonAnswer.Commands
{
    public class CommonAnswerUpdateCommand
    {
        public int Id { get; private set; }
        public string AnswerText { get;private set; }
        public int Priority { get; private set; }

        public CommonAnswerUpdateCommand(int id, string answerText, int priority)
        {
            Id = id;
            AnswerText = answerText;
            Priority = priority;
        }
    }
}
