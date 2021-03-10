using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Answers.AnswerMethod.Commands
{
    public class AnswerMethodCreateCommand
    {
        public string Title { get; private set; }

        public AnswerMethodCreateCommand(string title)
        {
            Title = title;
        }
    }
}
