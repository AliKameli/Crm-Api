using CRCIS.Web.INoor.CRM.Contract.Notifications;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Answers;
using CRCIS.Web.INoor.CRM.Contract.Repositories.ProductSubject;
using CRCIS.Web.INoor.CRM.Domain.Answers.Answering.Commands;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Masstransit.CaseSubject
{
    public class CaseSubjectConsumer : IConsumer<CaseSubjectUpdated>
    {
        private readonly IProductSubjectRepository _productSubjectRepository;

        public CaseSubjectConsumer(IProductSubjectRepository productSubjectRepository)
        {
            _productSubjectRepository = productSubjectRepository;
        }

        public async Task Consume(ConsumeContext<CaseSubjectUpdated> context)
        {
            await _productSubjectRepository.UpdateAsync(context.Message.SubjectId);
        }
    }
}
