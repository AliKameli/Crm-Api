using CRCIS.Web.INoor.CRM.Domain.Answers.CommonAnswer;
using CRCIS.Web.INoor.CRM.Domain.Answers.CommonAnswer.Commands;
using CRCIS.Web.INoor.CRM.Domain.Answers.CommonAnswer.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Answers.CommonAnswer.Queries;
using CRCIS.Web.INoor.CRM.Utility.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Repositories.Answers
{
    public interface ICommonAnswerRepository : IBaseRepository
    {
        Task<DataResponse<int>> CreateAsync(CommonAnswerCreateCommand command);
        Task<DataResponse<int>> DeleteAsync(int id);
        Task<DataResponse<IEnumerable<CommonAnswerGetDto>>> GetAsync(CommonAnswerDataTableQuery query);
        Task<DataResponse<CommonAnswerModel>> GetByIdAsync(int id);
        Task<DataResponse<int>> UpdateAsync(CommonAnswerUpdateCommand command);
    }
}
