﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Answers.CommonAnswer
{
    public class CommonAnswerModel
    {
        public int Id { get; set; }
        public string AnswerText { get; private set; }
        public int Priority { get; private set; }
    }
}
