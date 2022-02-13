using CRCIS.Web.INoor.CRM.Contract.Repositories.Permissions.Role;
using CRCIS.Web.INoor.CRM.Domain.Permissions.Role.Queris;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CRCIS.Web.INoor.CRM.Utility.Queries;
using System.Linq;
using CRCIS.Web.INoor.CRM.WebApi.Models.Role;
using AutoMapper;
using CRCIS.Web.INoor.CRM.Domain.Permissions.Role.Commands;
using Microsoft.AspNetCore.Authorization;

namespace CRCIS.Web.INoor.CRM.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoleController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;
        public RoleController(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageSize,
            [FromQuery] int pageIndex,
            [FromQuery] string sortField,
            [FromQuery] SortOrder? sortOrder)
        {
            var query = new RoleDataTableQuery(pageIndex, pageSize, sortField, sortOrder);
            var resonse = await _roleRepository.GetAsync(query);

            return Ok(resonse);
        }

        [HttpPost]
        public async Task<IActionResult> Post(RoleCreateModel model)
        {
            var command = _mapper.Map<RoleCreateCommand>(model);
            var response = await _roleRepository.CreateAsync(command);

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(RoleUpdateModel model)
        {
            var command = _mapper.Map<RoleUpdateCommand>(model);
            var response = await _roleRepository.UpdateAsync(command);

            return Ok(response);
        }
    }
}
