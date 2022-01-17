using AutoMapper;
using CRCIS.Web.INoor.CRM.Contract.Notifications;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Answers;
using CRCIS.Web.INoor.CRM.Domain.Answers.Answering.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Notify.Commands;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication.Extensions;
using CRCIS.Web.INoor.CRM.Utility.Response;
using CRCIS.Web.INoor.CRM.WebApi.Models.Answer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerCaseController : ControllerBase
    {
        private readonly IPendingHistoryRepository _pendingHistoryRepository;
        private readonly ICrmNotifyManager _crmNotifyManager;
        private readonly IMapper _mapper;
        private readonly IIdentity _identity;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AnswerCaseController(IMapper mapper, IIdentity identity,
            IPendingHistoryRepository pendingHistoryRepository,
            ICrmNotifyManager crmNotifyManager, IWebHostEnvironment hostEnvironment)
        {
            _mapper = mapper;
            _pendingHistoryRepository = pendingHistoryRepository;
            _identity = identity;
            _crmNotifyManager = crmNotifyManager;
            _hostEnvironment = hostEnvironment;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromForm] AnsweringCreateModel model)
        {
            var listAttachmentFiles = new List<AnsweringAttachmentItemDto>();

            if (model.Attachments is not null)
            {
                if (!Directory.Exists(Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot", "uploads")))
                {
                    Directory.CreateDirectory(Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot", "uploads"));
                }
                foreach (var formFile in model.Attachments)
                {
                    var attachmentItemDto = new AnsweringAttachmentItemDto
                    {
                        Name = formFile.FileName,
                        Address = $"{Guid.NewGuid()}{Path.GetExtension(formFile.FileName)}",
                    };
                    var filePath = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot", "uploads",attachmentItemDto.Address);
                    using var fileSteam = new FileStream(filePath, FileMode.Create);
                    await formFile.CopyToAsync(fileSteam);

                    listAttachmentFiles.Add(attachmentItemDto);
                }

            }
            model.AdminId = _identity.GetAdminId();
            var dto = _mapper.Map<AnsweringCreateDto>(model);
            dto.SetAttachments(listAttachmentFiles);

            var responseSave = await _pendingHistoryRepository.CreateAsync(dto);
            if (responseSave.Success == false)
                return Ok(responseSave);

            var command = new SendNotifiyCommand(model.AnswerMethodId,
                  model.CaseId,
                  model.AnswerSource,
                  model.AnswerText,
                  responseSave.Data,
                  listAttachmentFiles
                  );
            var response = await _crmNotifyManager.SendNotifyAsync(command);
            return Ok(response);
        }
    }
}
