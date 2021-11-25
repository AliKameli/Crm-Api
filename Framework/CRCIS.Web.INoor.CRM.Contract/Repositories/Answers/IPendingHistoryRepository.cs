using CRCIS.Web.INoor.CRM.Domain.Answers.Answering.Commands;
using CRCIS.Web.INoor.CRM.Domain.Answers.Answering.Dtos;
using CRCIS.Web.INoor.CRM.Utility.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Repositories.Answers
{
    public interface IPendingHistoryRepository
    {
        Task<DataResponse<long>> CreateAsync(AnsweringCreateDto dto);
        Task<DataResponse<int>> UpdateResulAsync(AnsweringUpdateResultCommand command);
    }
}
