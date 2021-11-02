using CRCIS.Web.INoor.CRM.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Cases.PendingCase.Dtos
{
    public class PendingCaseFullDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string NameFamiy { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public Guid NoorUserId { get; set; }
        public int ManualImportAdminId { get; set; }
        public DateTime ImportDateTime { get; set; }
        public string ImportDateTimePersian { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string CreateDateTimePersian { get; set; }
        public int SourceTypeId { get; set; }
        public int CaseStatusId { get; set; }
        public int ProductId { get; set; }
        public int AdminId { get; set; }
        public string ProductTitle { get; set; }
        public string SourceTypeTitle { get; set; }
        public string CaseStatusTitle { get; set; }
        public string ManualImportAdminName { get; set; }
        public string ManualImportAdminFamily { get; set; }
        public string Subjects { get; set; }
        public string MoreData { get; set; }

        public AnswerMethod? SuggestionAnswerMethod { get; set; }
        public string SuggestionAnswerSource { get; set; }
    }
}
