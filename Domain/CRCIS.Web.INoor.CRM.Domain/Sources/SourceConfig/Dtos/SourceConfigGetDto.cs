﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Sources.SourceConfig.Dtos
{
    public class SourceConfigGetDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ProductId { get; set; }
        public int SourceTypeId { get; set; }
        public int TotalCount { get; set; }
    }
}
