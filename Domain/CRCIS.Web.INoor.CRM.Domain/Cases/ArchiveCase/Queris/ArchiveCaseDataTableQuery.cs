﻿using CRCIS.Web.INoor.CRM.Utility.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Cases.ArchiveCase.Queris
{
    public class ArchiveCaseDataTableQuery : AbstractDataTableQuery
    {
        public string Order { get; private set; }
        public string SourceTypeTitle { get; private set; }
        public string ProductTitle { get;private set; }
        //public string FirstSubject { get; set; }
        public string Title { get; private set; }

        public ArchiveCaseDataTableQuery(int pageIndex, int pageSize, string sortField, SortOrder? sortOrder, string sourceTypeTitle,string productTitle, string title)
            : base(pageIndex, pageSize)
        {
            sortField = sortField?.Trim();
            if (!string.IsNullOrEmpty(sortField) && sortField != null)
            {
                Order = $"{sortField} {sortOrder.ToString()}";
            }

            SourceTypeTitle = sourceTypeTitle?.Trim();
            ProductTitle = productTitle?.Trim();
            Title = title?.Trim();
        }
    }
}
