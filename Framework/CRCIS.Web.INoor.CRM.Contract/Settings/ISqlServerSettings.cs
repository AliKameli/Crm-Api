﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Settings
{
    public interface ISqlServerSettings
    {
        string ConnectionString { get; set; }
    }
}
