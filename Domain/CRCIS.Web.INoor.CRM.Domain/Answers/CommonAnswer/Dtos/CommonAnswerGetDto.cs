using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Answers.CommonAnswer.Dtos
{
    public class CommonAnswerGetDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AnswerText { get; set; }
        public int Priority { get; private set; }
        public bool IsActive { get; set; }
        public int CreatorAdminId { get; set; }
        public int? ConfirmedAdminId { get; set; }
        public int RowNumber { get; set; }
        public int TotalCount { get; set; }
    }
}
