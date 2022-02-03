using CRCIS.Web.INoor.CRM.Contract.Repositories.Cases;
using CRCIS.Web.INoor.CRM.Contract.Service;
using CRCIS.Web.INoor.CRM.Domain.Cases.ArchiveCase.Commands;
using CRCIS.Web.INoor.CRM.Utility.Extensions;
using CRCIS.Web.INoor.CRM.Utility.Response;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Service
{
    public class ArchiveCaseService : IArchiveCaseService
    {
        private readonly IArchiveCaseRepository _archiveCaseRepository;
        private readonly ILogger _logger;
        public ArchiveCaseService(IArchiveCaseRepository archiveCaseRepository, ILoggerFactory loggerFactory)
        {
            _archiveCaseRepository = archiveCaseRepository;
            _logger = loggerFactory.CreateLogger<ArchiveCaseService>();
        }

        public async Task<DataResponse<int>> MoveCaseToAdminAsync(MoveCaseToCurrentAdminCardboardCommand command)
        {

            var resposne = new DataResponse<int>(false);
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    await _archiveCaseRepository.MoveCaseToAdminAsync(command);
                    await _archiveCaseRepository.DeleteAsync(command.CaseId);
                    await _archiveCaseRepository.AddCaseHistoryMoveCaseToAdminAsync(command);
                    transaction.Complete();

                    resposne = new DataResponse<int>(true);
                }
                catch (Exception ex)
                {
                    _logger.LogException(ex);
                    resposne = new DataResponse<int>(false);
                    resposne.AddError("خطایی در انتقال مورد رخ داده است");
                }

                return resposne;
            }
        }


        public async Task<DataResponse<int>> MoveCaseToAdminAsync(MoveCaseToCurrentAdminCardboardMultiCommand commandMulti)
        {

            var resposne = new DataResponse<int>(false);
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    foreach (var caseId in commandMulti.CaseIds)
                    {
                        var command = new MoveCaseToCurrentAdminCardboardCommand(commandMulti.AdminId, caseId);
                        await _archiveCaseRepository.MoveCaseToAdminAsync(command);
                        await _archiveCaseRepository.DeleteAsync(command.CaseId);
                        await _archiveCaseRepository.AddCaseHistoryMoveCaseToAdminAsync(command);

                    }
                    transaction.Complete();
                    resposne = new DataResponse<int>(true);
                }
                catch (Exception ex)
                {
                    _logger.LogException(ex);
                    resposne = new DataResponse<int>(false);
                    resposne.AddError("خطایی در انتقال مورد رخ داده است");
                }

                return resposne;
            }
        }


    }
}