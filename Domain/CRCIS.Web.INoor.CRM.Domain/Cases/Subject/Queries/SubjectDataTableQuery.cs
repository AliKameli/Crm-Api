﻿using CRCIS.Web.INoor.CRM.Utility.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Cases.Subject.Queries
{
    public class SubjectDataTableQuery : AbstractDataTableQuery
    {
        public SubjectDataTableQuery(int pageIndex, int pageSize) : base(pageIndex, pageSize)
        {
        }
    }
}