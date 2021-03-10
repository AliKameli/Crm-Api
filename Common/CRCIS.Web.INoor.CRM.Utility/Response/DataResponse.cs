using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Utility.Response
{
    public class DataResponse<TData>
    {
        public bool Success { get; private set; }
        public IList<string> ApiErrors { get; private set; }
        public string ApiError
        {
            get
            {
                if (this.ApiErrors == null || this.ApiErrors.Count == 0)
                {
                    return "";
                }
                else
                    return string.Join(" ", ApiErrors);
            }
        }
        public TData Data { get; private set; }
        public DataResponse(bool success)
        {
            this.Success = success;
        }
        public DataResponse(TData data)
        {
            this.Success = true;
            this.Data = data;
        }

        public DataResponse(IList<string> errors)
        {
            this.Success = false;
            this.ApiErrors = errors;
        }

        public DataResponse(bool success, IList<string> errors, TData data) : this(success)
        {
            ApiErrors = errors;
            Data = data;
        }

        public void AddError(string error)
        {
            this.ApiErrors.Add(error);
        }

    }
}

