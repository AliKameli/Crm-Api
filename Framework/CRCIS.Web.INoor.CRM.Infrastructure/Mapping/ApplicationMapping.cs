using AutoMapper;
using CRCIS.Web.INoor.CRM.Domain.Cases.CaseHistory.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Cases.CaseHistory.Queries;
using CRCIS.Web.INoor.CRM.Domain.Cases.PendingCase;
using CRCIS.Web.INoor.CRM.Domain.Cases.PendingCase.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Reports.Person.Dtos;
using CRCIS.Web.INoor.CRM.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Mapping
{
    public class ApplicationMapping : Profile
    {
        public ApplicationMapping()
        {
            CreateMap<PendingCaseModel, PendingCaseFullDto>()
                .ForMember(dest => dest.CreateDateTimePersian, opt => opt.MapFrom(src => src.CreateDateTime.ToPersinDateString(false, false)))
                .ForMember(dest => dest.CreateTimePersian, opt => opt.MapFrom(src => $"{src.CreateDateTime.Hour.ToString("00")}:{src.CreateDateTime.Minute.ToString("00")}"))
                .ForMember(dest => dest.ImportDateTimePersian, opt => opt.MapFrom(src => src.ImportDateTime.ToPersinDateString(true, false)))
                .ForMember(dest => dest.SuggestionAnswerMethod, opt => opt.Ignore())
                .ForMember(dest => dest.SuggestionAnswerSource, opt => opt.Ignore())
                ;

            CreateMap<CaseHistoriesQuery, CaseHistoriesDto>()
                .ForMember(dest => dest.OperationDateTimePersian, opt => opt.MapFrom(src => src.OperationDateTime.ToPersinDateString(true, false)));


            CreateMap<PersonReportResponse, PersonReportResponseFullDto>()
                .ForMember(dest => dest.AllowAssignToMe, opt => opt.Ignore())
                .ForMember(dest => dest.AllowAnswerByMe, opt => opt.Ignore())
                .ForMember(dest => dest.AllowAssignToOther, opt => opt.Ignore())
                .ForMember(dest => dest.AllowBackFromArchiveToMe, opt => opt.Ignore());
                            
        }
    }
}