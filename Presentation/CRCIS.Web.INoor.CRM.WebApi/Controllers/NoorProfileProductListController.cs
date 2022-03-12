using CRCIS.Web.INoor.CRM.Contract.Repositories.Sources;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication.ClientIpCheck;
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
    [ServiceFilter(typeof(ClientIpCheckActionFilter))]
    public class NoorProfileProductListController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public NoorProfileProductListController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _productRepository.GetDropDownListAsync();
            return Ok(response);
        }
    }
}
