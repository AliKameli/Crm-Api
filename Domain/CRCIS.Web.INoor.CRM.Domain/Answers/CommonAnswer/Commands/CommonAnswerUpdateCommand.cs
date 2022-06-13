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
        public string Title { get; private set; }
        public string AnswerText { get; private set; }
        public bool IsActive { get; private set; }
        public int Priority { get; private set; }
        public int ConfirmedAdminId { get;private set; }

        public CommonAnswerUpdateCommand(int id, string title, string answerText, bool isActive, int priority, int confirmedAdminId)
        {
            Id = id;
            Title = title;
            AnswerText = answerText;
            IsActive = isActive;
            Priority = priority;
            ConfirmedAdminId = confirmedAdminId;
        }
    }
}
