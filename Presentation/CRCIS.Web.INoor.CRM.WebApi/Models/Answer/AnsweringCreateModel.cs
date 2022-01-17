using CRCIS.Web.INoor.CRM.Domain.Answers.Answering.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.WebApi.Models.Answer
{
    public class AnsweringCreateModel
    {
        public string AnswerText { get; set; }
        public int AnswerMethodId { get; set; }
        public string AnswerSource { get; set; }
        public long CaseId { get; set; }
        public int AdminId { get; set; }
        public List<IFormFile> Attachments { get; set; }
    }
}