﻿using CRCIS.Web.INoor.CRM.Contract.Repositories.Reports;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication.Attributes;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class ReportDashboardCaseHistoryChartController : ControllerBase
    {
        private readonly IReportRepository _reportRepository;

        public ReportDashboardCaseHistoryChartController(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        [HttpGet]
        [JwtAuthorize]
        public async Task<IActionResult> Get()
        {
            var response = await _reportRepository.GetCaseHistoryReportAsync();
            return Ok(response);
        }
    }
}
