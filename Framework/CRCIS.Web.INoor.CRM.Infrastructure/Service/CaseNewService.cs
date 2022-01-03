using CRCIS.Web.INoor.CRM.Contract.Repositories.Cases;
using CRCIS.Web.INoor.CRM.Contract.Service;
using CRCIS.Web.INoor.CRM.Domain.Cases.ImportCase.Commands;
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
    public class CaseNewService : ICaseNewService
    {
        private readonly IImportCaseRepository _importCaseRepository;
        private readonly ILogger<CaseNewService> _logger;
        public CaseNewService(IImportCaseRepository importCaseRepository, ILoggerFactory loggerFactory)
        {
            _importCaseRepository = importCaseRepository;
            _logger = loggerFactory.CreateLogger<CaseNewService>();
        }
        /// <summary>
        /// single case
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<DataResponse<int>> MoveCaseToAdminAsync(MoveCaseToCurrentAdminCardboardCommand command)
        {
            var resposne = new DataResponse<int>(false);
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    await _importCaseRepository.MoveCaseToAdminAsync(command.CaseId);
                    await _importCaseRepository.DeleteCaseAsync(command.CaseId);
                    resposne = await _importCaseRepository.AddCaseHistoryMoveCaseToCurrentAdminAsync(command);
                    transaction.Complete();
                }
                catch (Exception e)
                {
                    _logger.LogException(e);
                    resposne = new DataResponse<int>(false);
                    resposne.AddError("خطایی در انتقال مورد رخ داده است");
                }
                return resposne;
            }
        }
        /// <summary>
        /// multi case
        /// </summary>
        /// <param name="commandMulti"></param>
        /// <returns></returns>
        public async Task<DataResponse<int>> MoveCaseToAdminAsync(MoveCaseToCurrentAdminCardboardMultiCommand commandMulti)
        {
            var resposne = new DataResponse<int>(false);
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    foreach (var caseId in commandMulti.CaseIds)
                    {
                        await _importCaseRepository.MoveCaseToAdminAsync(caseId);
                        await _importCaseRepository.DeleteCaseAsync(caseId);
                        var command = new MoveCaseToCurrentAdminCardboardCommand(commandMulti.AdminId, caseId);
                        resposne = await _importCaseRepository.AddCaseHistoryMoveCaseToCurrentAdminAsync(command);
                        if (resposne.Success == false)
                        {
                            throw resposne.Exception == null ? new Exception() : resposne.Exception;
                        }
                    }

                    transaction.Complete();
                    return new DataResponse<int>(true);
                }
                catch (Exception ex)
                {
                    _logger.LogException(ex);
                    resposne = new DataResponse<int>(false);
                    resposne.AddError("خطایی در انتقال مورد رخ داده است");
                    return resposne;
                }
            }
        }

        /// <summary>
        /// single
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<DataResponse<int>> MoveCaseToArchive(MoveCaseToArchiveCommand command)
        {
            var resposne = new DataResponse<int>(false);
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    await _importCaseRepository.MoveCaseToArchiveAsync(command.CaseId);
                    await _importCaseRepository.DeleteCaseAsync(command.CaseId);
                    await _importCaseRepository.AddCaseHistoryMoveCaseToArchiveAsync(command);

                    transaction.Complete();
                    return new DataResponse<int>(true);
                }
                catch (Exception ex)
                {
                    _logger.LogException(ex);
                    resposne = new DataResponse<int>(false);
                    resposne.AddError("خطایی در انتقال مورد رخ داده است");
                    return resposne;
                }
            }
        }

        /// <summary>
        /// multi
        /// </summary>
        /// <param name="commandMulti"></param>
        /// <returns></returns>
        public async Task<DataResponse<int>> MoveCaseToArchive(MoveCaseToArchiveMultiCommand commandMulti)
        {
            var resposne = new DataResponse<int>(false);
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    foreach (var caseId in commandMulti.CaseIds)
                    {
                        await _importCaseRepository.MoveCaseToArchiveAsync(caseId);
                        await _importCaseRepository.DeleteCaseAsync(caseId);
                        var command = new MoveCaseToArchiveCommand(caseId);
                        await _importCaseRepository.AddCaseHistoryMoveCaseToArchiveAsync(command);
                    }
                    transaction.Complete();
                    return new DataResponse<int>(true);
                }
                catch (Exception ex)
                {
                    _logger.LogException(ex);
                    resposne = new DataResponse<int>(false);
                    resposne.AddError("خطایی در انتقال مورد رخ داده است");
                    return resposne;
                }
            }
        }
    }
}
