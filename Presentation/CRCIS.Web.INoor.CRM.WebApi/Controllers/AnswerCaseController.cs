using AutoMapper;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Answers;
using CRCIS.Web.INoor.CRM.Domain.Answers.Answering.Dtos;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication.Extensions;
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
        private readonly IMapper _mapper;
        private readonly IIdentity _identity;

        public AnswerCaseController(IMapper mapper, IPendingHistoryRepository pendingHistoryRepository, IIdentity identity)
        {
            _mapper = mapper;
            _pendingHistoryRepository = pendingHistoryRepository;
            _identity = identity;
        }
        [HttpPost]
        public async Task<IActionResult> Post(AnsweringCreateModel model)
        {
            model.AdminId = _identity.GetAdminId();
            var dto = _mapper.Map<AnsweringCreateDto>(model);
            var response = await _pendingHistoryRepository.CreateAsync(dto);
            return Ok(response);
        }
    }
}
