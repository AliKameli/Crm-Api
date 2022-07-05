using AutoMapper;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Users;
using CRCIS.Web.INoor.CRM.Domain.Users.Admin.Commands;
using CRCIS.Web.INoor.CRM.Domain.Users.Admin.Queries;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication.Attributes;
using CRCIS.Web.INoor.CRM.Utility.Queries;
using CRCIS.Web.INoor.CRM.WebApi.Models.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;
        public AdminController(IAdminRepository adminRepository, IMapper mapper)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var response = await _adminRepository.GetByIdAsync(id);

            return Ok(response);
        }

        [HttpGet]
        [JwtAuthorize]
        public async Task<IActionResult> Get([FromQuery] int pageSize,
            [FromQuery] int pageIndex,
            [FromQuery] string sortField,
            [FromQuery] SortOrder? sortOrder)
        {
            var query = new AdminDataTableQuery(pageIndex, pageSize, sortField, sortOrder);
            var response = await _adminRepository.GetAsync(query);


            return Ok(response);
        }

        [HttpPost]
        [JwtAuthorize]
        public async Task<IActionResult> Post(AdminCreateModel model)
        {
            var command = _mapper.Map<AdminCreateCommand>(model);
            var response = await _adminRepository.CreateAsync(command);
            return Ok(response);
        }
        [HttpPut]
        [JwtAuthorize]
        public async Task<IActionResult> Put(AdminUpdateModel model)
        {
            var command = _mapper.Map<AdminUpdateCommand>(model);
            var response = await _adminRepository.UpdateAsync(command);
            return Ok(response);
        }

    }
}