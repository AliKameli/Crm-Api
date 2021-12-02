using AutoMapper;
using CRCIS.Web.INoor.CRM.Contract.Notifications;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Answers;
using CRCIS.Web.INoor.CRM.Domain.Answers.Answering.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Notify.Commands;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication.Extensions;
using CRCIS.Web.INoor.CRM.Utility.Response;
using CRCIS.Web.INoor.CRM.WebApi.Models.Answer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerCaseController : ControllerBase
    {
        private readonly IPendingHistoryRepository _pendingHistoryRepository;
        private readonly ICrmNotifyManager _crmNotifyManager;
        private readonly IMapper _mapper;
        private readonly IIdentity _identity;

        public AnswerCaseController(IMapper mapper, IIdentity identity,
            IPendingHistoryRepository pendingHistoryRepository,
            ICrmNotifyManager crmNotifyManager)
        {
            _mapper = mapper;
            _pendingHistoryRepository = pendingHistoryRepository;
            _identity = identity;
            _crmNotifyManager = crmNotifyManager;
        }

        [HttpPost]
        public async Task<IActionResult> Post(AnsweringCreateModel model)
        {
            model.AdminId = _identity.GetAdminId();
            var dto = _mapper.Map<AnsweringCreateDto>(model);
            var responseSave = await _pendingHistoryRepository.CreateAsync(dto);
            if (responseSave.Success == false)
                return Ok(responseSave);


          var command =   new SendNotifiyCommand(model.AnswerMethodId,
                model.CaseId,
                model.AnswerSource,
                model.AnswerText,
                responseSave.Data
                );
            var response = await _crmNotifyManager.SendNotifyAsync(command);

         
            return Ok(response);
        }
    }
}
