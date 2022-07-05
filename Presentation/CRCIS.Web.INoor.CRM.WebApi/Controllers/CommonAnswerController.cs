using AutoMapper;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Answers;
using CRCIS.Web.INoor.CRM.Domain.Answers.CommonAnswer.Commands;
using CRCIS.Web.INoor.CRM.Domain.Answers.CommonAnswer.Queries;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication.Attributes;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication.Extensions;
using CRCIS.Web.INoor.CRM.Utility.Queries;
using CRCIS.Web.INoor.CRM.WebApi.Models.CommonAnswer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
    public class CommonAnswerController : ControllerBase
    {
        private readonly ICommonAnswerRepository _commonAnswerRepository;
        private readonly IMapper _mapper;
        private readonly IIdentity _identity;
        public CommonAnswerController(IMapper mapper, ICommonAnswerRepository commonAnswerRepository, IIdentity identity)
        {
            _mapper = mapper;
            _commonAnswerRepository = commonAnswerRepository;
            _identity = identity;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var response = await _commonAnswerRepository.GetByIdAsync(id);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageSize,
            [FromQuery] int pageIndex,
            [FromQuery] string sortField,
            [FromQuery] SortOrder? sortOrder,
            [FromQuery] string title = null)
        {
            var query = new CommonAnswerDataTableQuery(pageIndex, pageSize, "", sortField, sortOrder,title);
            var response = await _commonAnswerRepository.GetAsync(query);

            return Ok(response);
        }

        [HttpPost]
        [JwtAuthorize]
        public async Task<IActionResult> Post(CommonAnswerCreateModel model)
        {
            model.CreatorAdminId = _identity.GetAdminId();
            var command = _mapper.Map<CommonAnswerCreateCommand>(model);
            var response = await _commonAnswerRepository.CreateAsync(command);
            return Ok(response);
        }

        [HttpPut]
        [JwtAuthorize]
        public async Task<IActionResult> Put(CommonAnswerUpdateModel model)
        {
            model.ConfirmedAdminId = _identity.GetAdminId();
            var command = _mapper.Map<CommonAnswerUpdateCommand>(model);
            var response = await _commonAnswerRepository.UpdateAsync(command);
            return Ok(response);
        }
        [HttpPatch]
        [JwtAuthorize]
        public async Task<IActionResult> Patch(CommonAnswerEditByOperatorPatchModel model)
        {
            var command = _mapper.Map<CommonAnswerEditByOperatorPatchCommand>(model);
            var resposne = await _commonAnswerRepository.EditByOperatoreAsync(command);
            return Ok(resposne);
        }

        [HttpDelete("{id}")]
        [JwtAuthorize]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _commonAnswerRepository.DeleteAsync(id);
            return Ok(response);
        }
    }
}
