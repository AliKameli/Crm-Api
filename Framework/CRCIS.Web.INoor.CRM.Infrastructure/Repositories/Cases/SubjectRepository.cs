﻿using CRCIS.Web.INoor.CRM.Contract.Repositories.Cases;
using CRCIS.Web.INoor.CRM.Data.Database;
using CRCIS.Web.INoor.CRM.Domain.Cases.Subject;
using CRCIS.Web.INoor.CRM.Domain.Cases.Subject.Commands;
using CRCIS.Web.INoor.CRM.Domain.Cases.Subject.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Cases.Subject.Queries;
using CRCIS.Web.INoor.CRM.Utility.Extensions;
using CRCIS.Web.INoor.CRM.Utility.Response;
using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Repositories.Cases
{
    public class SubjectRepository : BaseRepository, ISubjectRepository
    {
        protected override string TableName => "Subject";
        private readonly ILogger _logger;
        public SubjectRepository(ISqlConnectionFactory sqlConnectionFactory,ILoggerFactory loggerFactory) : base(sqlConnectionFactory)
        {
            _logger = loggerFactory.CreateLogger<SubjectRepository>();
        }
        public async Task<DataTableResponse<IEnumerable<SubjectGetDto>>> GetAsync(SubjectDataTableQuery query)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "List");

                var list =
                     await dbConnection
                    .QueryAsync<SubjectGetDto>(sql, query, commandType: CommandType.StoredProcedure);

                int totalCount = (list == null || !list.Any()) ? 0 : list.FirstOrDefault().TotalCount;
                var result = new DataTableResponse<IEnumerable<SubjectGetDto>>(list, totalCount);
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataTableResponse<IEnumerable<SubjectGetDto>>(errors);
                return result;
            }
        }

        public async Task<DataResponse<SubjectModel>> GetByIdAsync(int id)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "GetById");
                var command = new { Id = id };
                var subject =
                     await dbConnection
                    .QueryFirstOrDefaultAsync<SubjectModel>(sql, command, commandType: CommandType.StoredProcedure);

                if (subject != null)
                    return new DataResponse<SubjectModel>(subject);

                var errors = new List<string> { "وضعیت مورد یافت نشد" };
                var result = new DataResponse<SubjectModel>(errors);
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<SubjectModel>(errors);
                return result;
            }
        }
        public async Task<DataResponse<int>> CreateAsync(SubjectCreateCommand command)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "Create");

                var execute =
                     await dbConnection
                    .ExecuteAsync(sql, command, commandType: CommandType.StoredProcedure);

                return new DataResponse<int>(true);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<int>(errors);
                return result;
            }
        }
        public async Task<DataResponse<IEnumerable<SubjectDropDownListDto>>> GetDropDownListAsync()
        {

            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "DropDownList");

                var list =
                     await dbConnection
                    .QueryAsync<SubjectDropDownListDto>(sql, commandType: CommandType.StoredProcedure);

                list = list.Select(p => new SubjectDropDownListDto
                {
                    Id = p.Id,
                    Title = $"{ p.Title }  {p.Code}".Trim(),
                    Code = p.Code,
                });

                var result = new DataResponse<IEnumerable<SubjectDropDownListDto>>(list);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<IEnumerable<SubjectDropDownListDto>>(errors);
                return result;
            }
        }
        public async Task<DataResponse<IEnumerable<SubjectSearchDropDownListDto>>> GetSearchDropDownList(SubjectSearchDropDownQuery query)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "SearchDropDownList");

                var list =
                     await dbConnection
                    .QueryAsync<SubjectSearchDropDownListDto>(sql, query,commandType: CommandType.StoredProcedure);

                var resultList = list.Select(p => new SubjectSearchDropDownListDto
                {
                    Id = p.Id,
                    Title = $"{ p.Title }  {p.Code}".Trim(),
                    Code = p.Code,
                    Priority = p.Priority,
                    Weight = p.Weight,
                }).ToList();

                var result = new DataResponse<IEnumerable<SubjectSearchDropDownListDto>>(resultList);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<IEnumerable<SubjectSearchDropDownListDto>>(errors);
                return result;
            }
        }
        public async Task<DataResponse<int>> UpdateAsync(SubjectUpdateCommand command)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "Update");

                var execute =
                     await dbConnection
                    .ExecuteAsync(sql, command, commandType: CommandType.StoredProcedure);

                return new DataResponse<int>(true);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<int>(errors);
                return result;
            }
        }
        public async Task<DataResponse<int>> DeleteAsync(int id)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "Delete");
                var command = new { Id = id };

                await dbConnection
                    .QueryFirstOrDefaultAsync(sql, command, commandType: CommandType.StoredProcedure);

                return new DataResponse<int>(true);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<int>(errors);
                return result;
            }
        }
        public async Task<DataTableResponse<IEnumerable<SubjectChildrenGetDto>>> GetChildrenAsync(SubjectChildrenDataTableQuery query)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "Children");

                var list =
                     await dbConnection
                    .QueryAsync<SubjectChildrenGetDto>(sql, query, commandType: CommandType.StoredProcedure);

                int totalCount = (list == null || !list.Any()) ? 0 : list.FirstOrDefault().TotalCount;
                var result = new DataTableResponse<IEnumerable<SubjectChildrenGetDto>>(list, totalCount);
                //for (int i = 0; i < result.TotalCount; i++)
                //{
                //    result.Data.AsList()[i].RowNumber = i + 1;
                //}

                return result;

            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataTableResponse<IEnumerable<SubjectChildrenGetDto>>(errors);
                return result;
            }
        }

        public async Task<DataResponse<int>> UpdateSampleAsync()
        {
            try
            {
                var query = new SubjectDataTableQuery(1, 9999999);
                var list = await GetAsync(query);
                if (list.Success == false)
                {
                    var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                    var result = new DataResponse<int>(errors);
                    return result;
                }
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();
                dbConnection.Open();

                using var transaction = dbConnection.BeginTransaction();

                foreach (var subject in list.Data)
                {
                    var command = new SubjectUpdateCommand(
                        subject.Id,
                        subject.Title,
                        subject.ParentId,
                        subject.IsActive,
                        subject.Priority,
                        subject.Code);

                    var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "Update");

                    var execute =
                         await dbConnection
                        .ExecuteAsync(sql, command, commandType: CommandType.StoredProcedure,transaction: transaction);

                }
                transaction.Commit();
                return new DataResponse<int>(true);

            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<int>(errors);
                return result;
            }
        }

    }
}
