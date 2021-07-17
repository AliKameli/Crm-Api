using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Service
{
    public interface IImportService
    {
        Task InsertAsync(dynamic caseDto);


    }


    //public class EmailForwordService : IImportService
    //{
    //    private readonly 
    //    public EmailForwordService()
    //    {

    //    }
    //    public Task InsertAsync(dynamic caseDto)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
