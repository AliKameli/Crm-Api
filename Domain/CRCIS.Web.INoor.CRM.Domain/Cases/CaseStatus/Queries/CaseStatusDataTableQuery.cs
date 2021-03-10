﻿using CRCIS.Web.INoor.CRM.Utility.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Cases.CaseStatus.Queries
{
    public class CaseStatusDataTableQuery : AbstractDataTableQuery
    {
        public string Order { get; private set; }
        public CaseStatusDataTableQuery(int pageIndex, int pageSize, string sortField, SortOrder? sortOrder) 
            : base(pageIndex, pageSize)
        {
            sortField = sortField?.Trim();
            if (!string.IsNullOrEmpty(sortField) && sortField != null)
            {
                Order = $"{sortField} {sortOrder.ToString()}";
            }
        }
    }
}