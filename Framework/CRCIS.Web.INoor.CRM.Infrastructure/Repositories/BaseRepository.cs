using CRCIS.Web.INoor.CRM.Contract.Repositories;
using CRCIS.Web.INoor.CRM.Data.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Repositories
{
    public abstract class BaseRepository : IBaseRepository
    {
        /// <summary>
        /// Gets the table name.
        /// </summary>
        protected abstract string TableName { get; }

        /// <summary>
        /// ISqlConnectionFactory Injectetd from dependecy
        /// </summary>
        protected readonly ISqlConnectionFactory _sqlConnectionFactory;

        public BaseRepository(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
    }
}
