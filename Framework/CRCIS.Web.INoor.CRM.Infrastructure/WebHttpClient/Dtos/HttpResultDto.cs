using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.WebHttpClient.Dtos
{
    public class HttpResultDto<TData> where TData :class
    {
        public bool IsSuccess { get; private set; }

        public TData Data { get; private set; }
        public HttpStatusCode HttpStatusCode { get; private set; }

        public void Success(TData data)
        {
            IsSuccess = true;
            HttpStatusCode = HttpStatusCode.OK;
            Data = data;
        }

        public void Faild(HttpStatusCode statusCode)
        {
            IsSuccess = false;
            HttpStatusCode = statusCode;
            Data = null;
        }
    }
}
