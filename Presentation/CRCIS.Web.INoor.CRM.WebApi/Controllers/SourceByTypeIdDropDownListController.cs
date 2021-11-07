using CRCIS.Web.INoor.CRM.Contract.Repositories.Sources;
using CRCIS.Web.INoor.CRM.Domain.Sources.SourceConfig.Dtos;
using CRCIS.Web.INoor.CRM.Utility.Response;
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
    public class SourceByAnswerMethodIdDropDownListController : ControllerBase
    {
        private readonly ISourceConfigRepository _sourceConfigRepository;
        public SourceByAnswerMethodIdDropDownListController(ISourceConfigRepository sourceConfigRepository)
        {
            _sourceConfigRepository = sourceConfigRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var sourcesReponse = await _sourceConfigRepository.GetByAnswerMethodIdAsync(id);
            if (sourcesReponse.Success == false ||sourcesReponse.Data ==null )
            {
                var response = new DataResponse<string>(new List<string> { "خطا در واکشی اطلاعات " });
                return Ok(response);
            }
            Func<string, SourceConfigJsonDto> func = (strConfigjson) =>
               string.IsNullOrEmpty(strConfigjson) == true ? null :
               System.Text.Json.JsonSerializer.Deserialize<SourceConfigJsonDto>(strConfigjson);

            var list = sourcesReponse.Data.Select(s => new { s.Id, s.Title , Config = func(s.ConfigJson) });

            var list2 = list.Where(s => s.Config?.MailAddress != null).Where(s => s.Config.AllowSend == true)
                .Select(s => new { Id = s.Config.MailAddress, Title=  s.Title } as dynamic).ToList();

            var list3 = list.Where(s => s.Config?.SmsCenterPanelNumber != null).Where(s => s.Config.AllowSend == true)
                .Select(s => new { Id = s.Id.ToString(), Title=  s.Title } as dynamic).ToList();

            var list4 = list2.Concat(list3);
            return Ok(new DataResponse<IEnumerable<dynamic>>(true,null,list4 ));
        }

    }
}
