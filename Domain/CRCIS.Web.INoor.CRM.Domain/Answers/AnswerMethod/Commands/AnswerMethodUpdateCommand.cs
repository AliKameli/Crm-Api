using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Answers.AnswerMethod.Commands
{
    public class AnswerMethodUpdateCommand
    {
        public int Id { get; private set; }
        public string Title { get; private set; }

        public AnswerMethodUpdateCommand(int id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
