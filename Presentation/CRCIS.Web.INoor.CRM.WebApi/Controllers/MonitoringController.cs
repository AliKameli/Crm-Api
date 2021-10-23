﻿using CRCIS.Web.INoor.CRM.Contract.Repositories.Reports;
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
    public class MonitoringController : ControllerBase
    {
        private readonly IReportRepository _reportRepository;

        public MonitoringController(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _reportRepository.GetCaseHistoryReportAsync();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            return Ok();
        }
    }
}
