using CRCIS.Web.INoor.CRM.Domain.Cases.PendingCase.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Cases.RabbitImport.Commands;
using CRCIS.Web.INoor.CRM.Utility.Enums;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Specifications.Case
{
    public static class PendingCaseSuggestionsSpecification
    {
       
        public static PendingCaseFullDto PairSuggestionsForAnswer(this PendingCaseFullDto dto)
        {
            if (string.IsNullOrEmpty(dto.MoreData))
                return dto;

            try
            {
                var moreDataObject = System.Text.Json.JsonSerializer.Deserialize<ImportCaseMoreDataObject>(dto.MoreData);
                if (moreDataObject == null)
                    return dto;

                if (string.IsNullOrEmpty(moreDataObject.ToMailBox))
                    return dto;

                dto.SuggestionAnswerMethod = AnswerMethod.Email;
                dto.SuggestionAnswerSource = moreDataObject.ToMailBox;
                return dto;
            }
            catch (Exception)
            {
                return dto;
            }

        }
    }
}
