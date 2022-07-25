using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private IHostingEnvironment Environment;

        public FilesController(IHostingEnvironment environment)
        {
            Environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var files = new Dictionary<string, object> { };
            string[] filePaths = Directory.GetFiles(Path.Combine(this.Environment.WebRootPath, "mails/"));
            foreach (string file in filePaths)
            {
                files.Add(file, file);
            }

            return Ok(files);
        }
    }
}
