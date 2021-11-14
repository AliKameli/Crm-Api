using AutoMapper;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Sources;
using CRCIS.Web.INoor.CRM.Domain.Sources.Product.Commands;
using CRCIS.Web.INoor.CRM.Domain.Sources.Product.Queries;
using CRCIS.Web.INoor.CRM.Utility.Queries;
using CRCIS.Web.INoor.CRM.WebApi.Models.Product;
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
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var response = await _productRepository.GetByIdAsync(id);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageSize,
            [FromQuery] int pageIndex,
            [FromQuery] string sortField,
            [FromQuery] SortOrder? sortOrder,
            [FromQuery] string title = null, [FromQuery] string code = null,[FromQuery] string productTypeId = null)
        {
            var query = new ProductDataTableQuery(pageIndex, pageSize, sortField, sortOrder, title, code,productTypeId);
            var response = await _productRepository.GetAsync(query);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProductCreateModel model)
        {
            var command = _mapper.Map<ProductCreateCommand>(model);
            var response = await _productRepository.CreateAsync(command);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(ProductUpdateModel model)
        {
            var command = _mapper.Map<ProductUpdateCommand>(model);
            var response = await _productRepository.UpdateAsync(command);
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _productRepository.DeleteAsync(id);
            return Ok(response);
        }
    }
}
