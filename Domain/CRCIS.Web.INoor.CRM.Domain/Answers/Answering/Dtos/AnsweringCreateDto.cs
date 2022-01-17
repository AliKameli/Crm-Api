using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Answers.Answering.Dtos
{
    public class AnsweringCreateDto
    {
        public string AnswerText { get; private set; }
        public string AnswerSource { get; private set; }
        public int AnswerMethodId { get; private set; }
        public long CaseId { get; private set; }
        public int AdminId { get; private set; }
        public List<AnsweringAttachmentItemDto> Attachments { get; private set; }

        public AnsweringCreateDto(string answerText, string answerSource, int answerMethodId, long caseId, int adminId, List<AnsweringAttachmentItemDto> attachments)
        {
            AnswerText = answerText;
            AnswerSource = answerSource;
            AnswerMethodId = answerMethodId;
            CaseId = caseId;
            AdminId = adminId;
            Attachments = attachments;
        }
        public void SetAttachments (List<AnsweringAttachmentItemDto> attachments)
        {
            this.Attachments = attachments;
        }
    }
}
