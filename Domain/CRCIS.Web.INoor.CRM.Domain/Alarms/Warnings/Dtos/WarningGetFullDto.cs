using System;

namespace CRCIS.Web.INoor.CRM.Domain.Alarms.Warnings.Dtos
{
    public class WarningGetFullDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateDatePersian { get; set; }
        public DateTime VisitDate { get; set; }
        public string VisitDatePersian { get; set; }
        public int WarningTypeId { get; set; }
        public string WarningTypeTitle { get; set; }
        public long RowNumber { get; set; }
        public long TotalCount { get; set; }
    }
}
