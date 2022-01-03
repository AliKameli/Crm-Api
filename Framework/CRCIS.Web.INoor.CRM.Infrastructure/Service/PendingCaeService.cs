using CRCIS.Web.INoor.CRM.Contract.Repositories.Cases;
using CRCIS.Web.INoor.CRM.Contract.Service;
using CRCIS.Web.INoor.CRM.Domain.Cases.ImportCase.Commands;
using CRCIS.Web.INoor.CRM.Domain.Cases.PendingCase.Commands;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication.Extensions;
using CRCIS.Web.INoor.CRM.Utility.Extensions;
using CRCIS.Web.INoor.CRM.Utility.Response;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Service
{
    public class PendingCaeService : IPendingCaseService
    {
        private readonly IIdentity _identity;
        private readonly ILogger<PendingCaeService> _logger;
        private readonly IPendingCaseRepository _pendingCaseRepository;
        public PendingCaeService(IPendingCaseRepository pendingCaseRepository, ILoggerFactory loggerFactory, IIdentity identity)
        {
            _logger = loggerFactory.CreateLogger<PendingCaeService>();
            _pendingCaseRepository = pendingCaseRepository;
            _identity = identity;
        }


        public async Task<DataResponse<int>> MoveCaseToArchiveAsync(MoveCaseToArchiveCommand command)
        {
            var adminId = _identity.GetAdminId();
            var resposne = new DataResponse<int>(false);
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    await _pendingCaseRepository.MoveCaseToArchiveAsync(command.CaseId);
                    await _pendingCaseRepository.DeleteCaseAsync(command.CaseId);
                    await _pendingCaseRepository.AddCaseHistoryMoveCaseToArchive(adminId, command.CaseId);

                    transaction.Complete();
                    resposne = new DataResponse<int>(true);
                }
                catch (Exception ex)
                {
                    _logger.LogException(ex);
                    resposne = new DataResponse<int>(false);
                    resposne.AddError("خطایی در آرشیو مورد رخ داده است");
                }
            }
            return resposne;
        }
        public async Task<DataResponse<int>> MoveCaseToArchiveAsync(MoveCaseToArchiveMultiCommand commandMulti)
        {
            var adminId = _identity.GetAdminId();
            var resposne = new DataResponse<int>(false);
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    foreach (var caseId in commandMulti.CaseIds)
                    {
                        await _pendingCaseRepository.MoveCaseToArchiveAsync(caseId);
                        await _pendingCaseRepository.DeleteCaseAsync(caseId);
                        await _pendingCaseRepository.AddCaseHistoryMoveCaseToArchive(adminId, caseId);
                    }

                    transaction.Complete();
                    resposne = new DataResponse<int>(true);
                }
                catch (Exception ex)
                {
                    _logger.LogException(ex);
                    resposne = new DataResponse<int>(false);
                    resposne.AddError("خطایی در آرشیو مورد رخ داده است");
                }
                return resposne;
            }
        }

        public async Task<DataResponse<int>> MoveCaseToAdminAsync(MoveCaseToPartnerAdminCardboardCommand command)
        {
            var adminId = _identity.GetAdminId();
            var resposne = new DataResponse<int>(false);
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    await _pendingCaseRepository.MoveCaseToAdminAsync(command);
                    await _pendingCaseRepository.AddCaseHistoryMoveCaseToAdminAsync(fromAdminId: adminId, command.AdminId, command.CaseId);
                }
                catch (Exception ex)
                {
                    _logger.LogException(ex);
                    resposne = new DataResponse<int>(false);
                    resposne.AddError("خطایی در انتقال مورد رخ داده است");
                }
            }
            return resposne;
        }
        public async Task<DataResponse<int>> MoveCaseToAdminAsync(MoveCaseToPartnerAdminCardboardMultiCommand commandMulti)
        {
            var adminId = _identity.GetAdminId();
            var resposne = new DataResponse<int>(false);
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    foreach (var caseId in commandMulti.CaseIds)
                    {
                        var command = new MoveCaseToPartnerAdminCardboardCommand(commandMulti.AdminId, caseId);
                        await _pendingCaseRepository.MoveCaseToAdminAsync(command);
                        await _pendingCaseRepository.AddCaseHistoryMoveCaseToAdminAsync(fromAdminId: adminId, command.AdminId, command.CaseId);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogException(ex);
                    resposne = new DataResponse<int>(false);
                    resposne.AddError("خطایی در انتقال مورد رخ داده است");
                }
            }
            return resposne;
        }
    }
}