using CRCIS.Web.INoor.CRM.Utility.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Repositories.ProductSubject
{
    public interface IProductSubjectRepository
    {
        Task<DataResponse<int>> UpdateAsync(int subjectId);
    }
}
