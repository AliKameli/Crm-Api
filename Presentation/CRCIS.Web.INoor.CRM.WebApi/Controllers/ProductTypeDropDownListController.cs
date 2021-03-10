using CRCIS.Web.INoor.CRM.Contract.Repositories.Sources;
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
    public class ProductTypeDropDownListController : ControllerBase
    {
        private readonly IProductTypeRepository _productTypeRepository;

        public ProductTypeDropDownListController(IProductTypeRepository productTypeRepository)
        {
            _productTypeRepository = productTypeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response =await _productTypeRepository.GetDropDownListAsync();
            return Ok(response);
        }
    }
}
