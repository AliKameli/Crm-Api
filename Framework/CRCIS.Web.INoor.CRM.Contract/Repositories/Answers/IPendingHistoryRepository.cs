using CRCIS.Web.INoor.CRM.Domain.Answers.Answering.Dtos;
using CRCIS.Web.INoor.CRM.Utility.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Repositories.Answers
{
    public interface IPendingHistoryRepository : IBaseRepository
    {
        Task<DataResponse<int>> CreateAsync(AnsweringCreateDto dto);
    }
}
