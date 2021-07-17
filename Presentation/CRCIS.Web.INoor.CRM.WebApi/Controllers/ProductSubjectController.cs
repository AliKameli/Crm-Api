//using AutoMapper;
//using CRCIS.Web.INoor.CRM.Domain.Sources.Product.Commands;
//using CRCIS.Web.INoor.CRM.WebApi.Models.ProductSubject;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace CRCIS.Web.INoor.CRM.WebApi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ProductSubjectController : ControllerBase
//    {
//        private readonly IMapper _mapper;
//        public ProductSubjectController()
//        {

//        }
//        [HttpPut]
//        public async Task<IActionResult> Put(SubjectProductModel model)
//        {
//            var command = _mapper.Map<ProductUpdateCommand>(model);
//            var response = await _productRepository.UpdateAsync(command);
//            return Ok(response);
//        }
//    }
//}
