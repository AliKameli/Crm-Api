using CRCIS.Web.INoor.CRM.Domain.Answers.CommonAnswer;
using CRCIS.Web.INoor.CRM.Domain.Answers.CommonAnswer.Commands;
using CRCIS.Web.INoor.CRM.Domain.Answers.CommonAnswer.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Answers.CommonAnswer.Queries;
using CRCIS.Web.INoor.CRM.Utility.Dtos;
using CRCIS.Web.INoor.CRM.Utility.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Repositories.Answers
{
    public interface ICommonAnswerRepository
    {
        Task<DataResponse<int>> CreateAsync(CommonAnswerCreateCommand command);
        Task<DataResponse<int>> DeleteAsync(int id);
        Task<DataResponse<int>> EditByOperatoreAsync(CommonAnswerEditByOperatorPatchCommand command);
        Task<DataResponse<IEnumerable<CommonAnswerGetFullDto>>> GetAsync(CommonAnswerDataTableQuery query);
        Task<DataResponse<CommonAnswerModel>> GetByIdAsync(int id);
        Task<DataResponse<IEnumerable<DropDownListDto>>> GetDropDownListAsync();
        Task<DataResponse<int>> UpdateAsync(CommonAnswerUpdateCommand command);
    }
}
