using CRCIS.Web.INoor.CRM.Contract.Repositories.Cases;
using CRCIS.Web.INoor.CRM.Contract.Service;
using CRCIS.Web.INoor.CRM.Domain.Cases.CaseSubject.Commands;
using CRCIS.Web.INoor.CRM.Domain.Cases.CaseSubject.Dtos;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication.Extensions;
using CRCIS.Web.INoor.CRM.Infrastructure.Masstransit.CaseSubject;
using CRCIS.Web.INoor.CRM.Utility.Extensions;
using CRCIS.Web.INoor.CRM.Utility.Response;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using MassTransit;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Service
{
    public class CaseSubjectService : ICaseSubjectService
    {
        private readonly ILogger<CaseSubjectService> _logger;
        private readonly ICaseSubjectRepository _caseSubjectRepository;

        private readonly IBus _bus;

        public CaseSubjectService(ILoggerFactory loggerFactory, ICaseSubjectRepository caseSubjectRepository, IBus bus)
        {
            _logger = loggerFactory.CreateLogger<CaseSubjectService>();
            _caseSubjectRepository = caseSubjectRepository;
            _bus = bus;
        }

        public async Task<DataResponse<IEnumerable<CaseSubjectFullDto>>> AddSubjectAsync(UpdateCaseAddSubjectCommand command)
        {
            var response = new DataResponse<IEnumerable<CaseSubjectFullDto>>(false);
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    await _caseSubjectRepository.UpdateCaseAddSubjectAsync(command);
                    var caseSubjects = await _caseSubjectRepository.GetCaseSubjectsByCaseIdAsync(command.CaseId);

                    var validate = this.ValidateVariant(caseSubjects);
                    if (validate == false)
                    {
                        response = new DataResponse<IEnumerable<CaseSubjectFullDto>>(false);
                        response.AddError("باید حداقل یک موضوع انتخاب شود");

                        return response;// ignore commit trans
                    }

                    transaction.Complete();
                    response = new DataResponse<IEnumerable<CaseSubjectFullDto>>(caseSubjects);
                }
                catch (Exception ex)
                {
                    _logger.LogException(ex);

                    response = new DataResponse<IEnumerable<CaseSubjectFullDto>>(false);
                    response.AddError("خطایی در اضافه کردن موضوع رخ داده است");
                }
            }

            try
            {
                var @event= new CaseSubjectUpdated(command.SubjectId);
                await _bus.Publish(@event);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
            }

            return response;
        }

        public async Task<DataResponse<IEnumerable<CaseSubjectFullDto>>> RemoveSubjectAsync(UpdateCaseRemoveSubjectCommand command)
        {
            var response = new DataResponse<IEnumerable<CaseSubjectFullDto>>(false);
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    await _caseSubjectRepository.UpdateCaseRemoveSubjectAsync(command);
                    var caseSubjects = await _caseSubjectRepository.GetCaseSubjectsByCaseIdAsync(command.CaseId);

                    var validate = this.ValidateVariant(caseSubjects);
                    if (validate == false)
                    {
                        response = new DataResponse<IEnumerable<CaseSubjectFullDto>>(false);
                        response.AddError("باید حداقل یک موضوع انتخاب شود");

                        return response;// ignore commit transaction by return here
                    }

                    transaction.Complete();
                    response = new DataResponse<IEnumerable<CaseSubjectFullDto>>(caseSubjects);
                }
                catch (Exception ex)
                {
                    _logger.LogException(ex);

                    response = new DataResponse<IEnumerable<CaseSubjectFullDto>>(false);
                    response.AddError("خطایی در اضافه کردن موضوع رخ داده است");
                }
            }

            try
            {
                var @event = new CaseSubjectUpdated(command.SubjectId);
                await _bus.Publish(@event);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
            }

            return response;
        }

        private bool ValidateVariant(IEnumerable<CaseSubjectFullDto> caseSubjectEntities)
        {
            var countPrimary = caseSubjectEntities.Count(a => a.IsPrimary == true);

            return countPrimary == 1;
        }
    }
}
