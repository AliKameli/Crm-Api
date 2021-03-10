﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Cases.CaseSubject.Commands
{
    public class CaseSubjectCreateCommand
    {
        public string Title { get;private set; }

        public CaseSubjectCreateCommand(string title)
        {
            Title = title;
        }
    }
}
