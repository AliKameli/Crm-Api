using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Answers.AnswerMethod.Dtos
{
    public class AnswerMethodGetDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int RowNumber { get; set; }
        public int TotalCount { get; set; }
    }
}
