using CRCIS.Web.INoor.CRM.Domain.Answers.AnswerMethod;
using CRCIS.Web.INoor.CRM.Domain.Answers.AnswerMethod.Commands;
using CRCIS.Web.INoor.CRM.Domain.Answers.AnswerMethod.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Answers.AnswerMethod.Queris;
using CRCIS.Web.INoor.CRM.Utility.Dtos;
using CRCIS.Web.INoor.CRM.Utility.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Repositories.Answers
{
    public interface IAnswerMethodRepository 
    {
        Task<DataResponse<int>> CreateAsync(AnswerMethodCreateCommand command);
        Task<DataResponse<int>> DeleteAsync(int id);
        Task<DataResponse<IEnumerable<AnswerMethodGetDto>>> GetAsync(AnswerMethodDataTableQuery query);
        Task<DataResponse<AnswerMethodModel>> GetByIdAsync(int id);
        Task<DataResponse<IEnumerable<DropDownListDto>>> GetDropDownListAsync();
        Task<DataResponse<int>> UpdateAsync(AnswerMethodUpdateCommand command);
    }
}
