using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Utility.Extensions
{
    public static class LoggerExtensions
    {
        public static void LogException(this ILogger logger, Exception exception)
        {
            if (logger is null)
            {
                return;
            }
            if (exception is null)
            {
                return;
            }
            logger.LogError(exception.Message);
            logger.LogError(exception.StackTrace);

            if (exception.InnerException is null)
            {
                return;
            }
            logger.LogError(exception.InnerException.Message);
            logger.LogError(exception.InnerException.StackTrace);
        }
    }
}
