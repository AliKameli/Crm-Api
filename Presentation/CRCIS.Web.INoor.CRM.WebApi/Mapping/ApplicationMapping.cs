using AutoMapper;
using CRCIS.Web.INoor.CRM.Domain.Answers.Answering.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Answers.AnswerMethod.Commands;
using CRCIS.Web.INoor.CRM.Domain.Answers.CommonAnswer.Commands;
using CRCIS.Web.INoor.CRM.Domain.Cases.CaseStatus.Commands;
using CRCIS.Web.INoor.CRM.Domain.Cases.CaseSubject.Commands;
using CRCIS.Web.INoor.CRM.Domain.Cases.ImportCase.Commands;
using CRCIS.Web.INoor.CRM.Domain.Cases.OperationType.Commands;
using CRCIS.Web.INoor.CRM.Domain.Cases.PendingCase.Commands;
using CRCIS.Web.INoor.CRM.Domain.Cases.Subject.Commands;
using CRCIS.Web.INoor.CRM.Domain.Permissions.Role.Commands;
using CRCIS.Web.INoor.CRM.Domain.Sources.Product.Commands;
using CRCIS.Web.INoor.CRM.Domain.Sources.ProductType.Commands;
using CRCIS.Web.INoor.CRM.Domain.Sources.SourceTypes.Commands;
using CRCIS.Web.INoor.CRM.Domain.Users.Admin.Commands;
using CRCIS.Web.INoor.CRM.Domain.Users.Admin.Queries;
using CRCIS.Web.INoor.CRM.WebApi.Models.Account;
using CRCIS.Web.INoor.CRM.WebApi.Models.Admin;
using CRCIS.Web.INoor.CRM.WebApi.Models.Answer;
using CRCIS.Web.INoor.CRM.WebApi.Models.AnswerMethod;
using CRCIS.Web.INoor.CRM.WebApi.Models.Case;
using CRCIS.Web.INoor.CRM.WebApi.Models.CaseStatus;
using CRCIS.Web.INoor.CRM.WebApi.Models.CommonAnswer;
using CRCIS.Web.INoor.CRM.WebApi.Models.OpreationType;
using CRCIS.Web.INoor.CRM.WebApi.Models.Product;
using CRCIS.Web.INoor.CRM.WebApi.Models.ProductType;
using CRCIS.Web.INoor.CRM.WebApi.Models.Role;
using CRCIS.Web.INoor.CRM.WebApi.Models.SourceType;
using CRCIS.Web.INoor.CRM.WebApi.Models.Subject;
using CRCIS.Web.INoor.CRM.WebApi.Models.UpdateCaseSubject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRCIS.Web.INoor.CRM.WebApi.Mapping
{
    public class ApplicationMapping : Profile
    {
        public ApplicationMapping()
        {
            ShouldMapField = fieldInfo => true;
            ShouldMapProperty = propertyInfo => true;

            #region CaseStatus
            CreateMap<CaseStatusCreateModel, CaseStatusCreateCommand>();
            CreateMap<CaseStatusUpdateModel, CaseStatusUpdateCommand>();
            #endregion

            #region CommonAnswer
            CreateMap<CommonAnswerCreateModel, CommonAnswerCreateCommand>();
            CreateMap<CommonAnswerUpadateModel, CommonAnswerUpdateCommand>();
            #endregion

            #region AnswerMethod
            CreateMap<AnswerMethodCreateModel, AnswerMethodCreateCommand>();
            CreateMap<AnswerMethodUpdateModel, AnswerMethodUpdateCommand>();
            #endregion

            #region Subject
            CreateMap<SubjectCreateModel, SubjectCreateCommand>()
                .ConvertUsing(src => mapSubjectCreateModelToSubjectCreateCommand(src));
            CreateMap<SubjectUpdateModel, SubjectUpdateCommand>()
                .ConvertUsing(src => mapSubjectUpdateModelToSubjectUpateCommand(src));
            #endregion

            #region CaseSubject
            CreateMap<UpdateCaseAddSubjectModel, UpdateCaseAddSubjectCommand>();
            CreateMap<UpdateCaseRemoveSubjectModel, UpdateCaseRemoveSubjectCommand>();
            #endregion

            #region ProductType
            CreateMap<ProductTypeCreateModel, ProductTypeCreateCommand>();
            CreateMap<ProductTypeUpdateModel, ProductTypeUpdateCommand>();
            #endregion

            #region Product
            CreateMap<ProductCreateModel, ProductCreateCommand>();
            CreateMap<ProductUpdateModel, ProductUpdateCommand>();
            #endregion

            #region SourceConfig
            CreateMap<SourceTypeCreateModel, SourceTypeCreateCommand>();
            CreateMap<SourceTypeUpdateModel, SourceTypeUpdateCommand>();
            #endregion

            #region OperationType
            CreateMap<OperationTypeCreateModel, OperationTypeCreateCommand>();
            CreateMap<OperationTypeUpdateModel, OperationTypeUpdateCommand>();
            #endregion

            #region CaseNew
            CreateMap<CaseCreateModel, ImportCaseCreateCommand>()
                .ConvertUsing(src => mapImportCaseCreateModelToImportCaseCreateCommand(src));
            #endregion

            #region CasePending
            CreateMap<CaseUpdateModel, PendingCaseUpdateCommand>()
                 .ConvertUsing(src => mapImportCaseUpdateModelToImportCaseUpdateCommand(src));
            CreateMap<CaseUpdateProductModel, CaseUpdateProductCommand>();
            #endregion

            #region Admin
            CreateMap<AdminCreateModel, AdminCreateCommand>()
                .ConvertUsing(src => mapAdminCreateModelToAdminCreateCommand(src));
            CreateMap<AdminUpdateModel, AdminUpdateCommand>();
            CreateMap<LoginModel, AdminLoginQuery>();
            #endregion

            #region Answering
            CreateMap<AnsweringCreateModel, AnsweringCreateDto>()
                .ConvertUsing(src => mapAnsweringCreateModelToAnsweringCreateDto(src));
            #endregion

            #region Role
            CreateMap<RoleCreateModel, RoleCreateCommand>();
            CreateMap<RoleUpdateModel, RoleUpdateCommand>();
            #endregion

        }


        private CommonAnswerCreateCommand mapCommonAnswerCreateModelToCommonAnswerCreateCommand(CommonAnswerCreateModel model)
        {
            return new CommonAnswerCreateCommand(model.Title, model.AnswerText, model.Priority);
        }


        private ImportCaseCreateCommand mapImportCaseCreateModelToImportCaseCreateCommand(CaseCreateModel model)
        {
            Guid noorUserId;
            Guid.TryParse(model.NoorUserId, out noorUserId);
            return new ImportCaseCreateCommand(
                model.Title,
                model.NameFamily,
                model.Email,
                model.Description,
                model.SourceTypeId,
                noorUserId,
                model.ProductId,
                model.ManualImportAdminId,
                model.Mobile
                );
        }

        private PendingCaseUpdateCommand mapImportCaseUpdateModelToImportCaseUpdateCommand(CaseUpdateModel model)
        {
            string subjectIds = "";
            if (model.SubjectIds != null && model.SubjectIds.Any())
            {
                subjectIds = string.Join(',', model.SubjectIds.ToArray());
            }

            return new PendingCaseUpdateCommand(
                model.Id,
                model.Title,
                model.NameFamily,
                model.Email,
                model.Description,
                model.Mobile,
                model.SourceTypeId,
                model.ProductId,
                subjectIds
                );
        }

        private AdminCreateCommand mapAdminCreateModelToAdminCreateCommand(AdminCreateModel model)
        {
            return new AdminCreateCommand(
                model.Username,
                model.Password,
                model.Name,
                model.Family,
                model.Mobile,
                model.NoorPersonId);
        }

        private SubjectCreateCommand mapSubjectCreateModelToSubjectCreateCommand(SubjectCreateModel model)
        {
            return new SubjectCreateCommand(model.Title, model.ParentId, model.IsActive, model.Priority, model.Code);
        }

        private SubjectUpdateCommand mapSubjectUpdateModelToSubjectUpateCommand(SubjectUpdateModel model)
        {
            return new SubjectUpdateCommand(
                model.Id,
                model.Title,
                model.ParentId,
                model.IsActive,
                model.Priority,
                model.Code
                );
        }

        private AnsweringCreateDto mapAnsweringCreateModelToAnsweringCreateDto(AnsweringCreateModel model)
        {
            return new AnsweringCreateDto(
                model.AnswerText,
                model.AnswerSource,
                model.AnswerMethodId,
                model.CaseId,
                model.AdminId,
                null
                );
        }
    }
}
