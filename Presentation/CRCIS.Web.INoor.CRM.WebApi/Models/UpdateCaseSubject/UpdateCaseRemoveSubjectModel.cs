using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.WebApi.Models.UpdateCaseSubject
{
    public class UpdateCaseRemoveSubjectModel
    {
        public int SubjectId { get; set; }
        public long CaseId { get; set; }
    }
}