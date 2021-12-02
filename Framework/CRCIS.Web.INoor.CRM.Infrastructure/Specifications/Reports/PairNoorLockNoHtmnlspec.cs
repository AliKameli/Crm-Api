using CRCIS.Web.INoor.CRM.Domain.Reports.NoorLock.Dtos;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Specifications.Reports
{
    public static class NoorLockCaseReportDtoSpecifications
    {
        public static NoorLockCaseReportDto PairNoorLockCaseReportNotHtmlAnswer(this NoorLockCaseReportDto dto)
        {
            if (string.IsNullOrEmpty(dto?.AnswerText))
            {
                return dto;
            }
            dto.AnswerTextNoHtml = dto.AnswerText.Replace("<br/>", "\r\n");
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(dto.AnswerTextNoHtml);
            // call one of the doc.LoadXXX() functions
            dto.AnswerTextNoHtml= doc.DocumentNode.InnerText;
            return dto;
        }
    }
}
