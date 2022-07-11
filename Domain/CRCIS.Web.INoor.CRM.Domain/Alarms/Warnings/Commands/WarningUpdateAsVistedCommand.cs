using System;

namespace CRCIS.Web.INoor.CRM.Domain.Alarms.Warnings.Commands
{
    public class WarningUpdateAsVistedCommand
    {
        public long Id { get; set; }
        public int  VisitedAdminId { get; set; }
        public DateTime VisitedDate { get; set; }

        public WarningUpdateAsVistedCommand(long id, int visitedAdminId)
        {
            Id = id;
            VisitedAdminId = visitedAdminId;
            VisitedDate = DateTime.Now;
        }
    }
}
