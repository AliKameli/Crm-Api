﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Cases.ArchiveCase.Dtos
{
    public class ArchiveCaseGetDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string ProductTitle { get; set; }
        public long RowNumber { get; set; }
        public string SourceTypeTitle { get; set; }
        public long TotalCount { get; set; }
    }
}
