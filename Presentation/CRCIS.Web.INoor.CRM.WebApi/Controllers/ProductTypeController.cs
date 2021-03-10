using AutoMapper;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Sources;
using CRCIS.Web.INoor.CRM.Domain.Sources.ProductType.Commands;
using CRCIS.Web.INoor.CRM.Domain.Sources.ProductType.Queries;
using CRCIS.Web.INoor.CRM.Utility.Queries;
using CRCIS.Web.INoor.CRM.WebApi.Models.ProductType;
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
    public class ProductTypeController : ControllerBase
    {
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly IMapper _mapper;

        public ProductTypeController(IProductTypeRepository productTypeRepository, IMapper mapper)
        {
            _productTypeRepository = productTypeRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var response = await _productTypeRepository.GetByIdAsync(id);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageSize,
            [FromQuery] int pageIndex,
            [FromQuery] string sortField,
            [FromQuery] SortOrder? sortOrder)
        {
            var query = new ProductTypeDataTableQuery(pageIndex, pageSize, sortField, sortOrder);
            var resonse = await _productTypeRepository.GetAsync(query);

            return Ok(resonse);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProductTypeCreateModel model)
        {
            var command = _mapper.Map<ProductTypeCreateCommand>(model);
            var response = await _productTypeRepository.CreateAsync(command);

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(ProductTypeUpdateModel model)
        {
            var command = _mapper.Map<ProductTypeUpdateCommand>(model);
            var response = await _productTypeRepository.UpdateAsync(command);

            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _productTypeRepository.DeleteAsync(id);
            return Ok(response);
        }
    }
}
