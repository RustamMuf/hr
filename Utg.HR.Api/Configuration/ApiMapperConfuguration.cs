using AutoMapper;
using System;
using Utg.HR.Common.Models.ClientModel;
using Utg.HR.Common.Models.ClientModel.Import;
using Utg.HR.Common.Models.Domain;

namespace Utg.HR.Api.Configuration
{
    public static class ApiMapperConfiguration
    {
        public static void ConfigureMappings(IMapperConfigurationExpression config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            config.CreateMap<BalanceVacation, BalanceVacationViewModel>()
                .ForMember(item => item.BalanceId, exp => exp.MapFrom(item => item.BalanceId))
                .ForMember(item => item.BalanceOfVacation, exp => exp.MapFrom(item => item.BalanceOfVacation))
                .ForMember(item => item.UserProfileId, exp => exp.MapFrom(item => item.UserProfileId));


            config.CreateMap<VacationRequest, VacationRequestHistoryChange>()
                .ForMember(item => item.ChangeDate, exp => exp.MapFrom(item => DateTime.Now))
                .ForMember(item => item.Comment, exp => exp.MapFrom(item => item.Comment))
                .ForMember(item => item.RequestId, exp => exp.MapFrom(item => item.Id))
                .ForMember(item => item.State, exp => exp.MapFrom(item => item.VacationRequestState))
                .ForMember(item => item.AnswerComment, exp => exp.MapFrom(item => item.AnswerComment))
                .ForMember(item => item.VacationRequest, exp => exp.Ignore())
                .ForMember(item => item.VacationRequestHistoryChangeId, exp => exp.Ignore());


            config.CreateMap<VacationOrder, VacationOrderViewModel>()
                .ForMember(item => item.VacationId, exp => exp.MapFrom(item => item.Id))
                .ForMember(item => item.CreatedDate, exp => exp.MapFrom(item => item.CreatedDate))
                .ForMember(item => item.Id, exp => exp.MapFrom(item => item.Id))
                .ForMember(item => item.StartDate, exp => exp.MapFrom(item => item.Vacation.StartDate))
                .ForMember(item => item.EndDate, exp => exp.MapFrom(item => item.Vacation.EndDate))
                .ForMember(item => item.OrderPrinted, exp => exp.MapFrom(item => item.OrderPrinted))
                .ForMember(item => item.OrderRecived, exp => exp.MapFrom(item => item.OrderRecived))
                .ForMember(item => item.VacationType, exp => exp.MapFrom(item => item.Vacation.VacationType))
                .ForMember(item => item.UserProfileId, exp => exp.MapFrom(item => item.Vacation.UserProfileId))
                .ForMember(item => item.PositionName, exp => exp.Ignore())
                .ForMember(item => item.DepartmentName, exp => exp.Ignore())
                .ForMember(item => item.CompanyName, exp => exp.Ignore())
                .ForMember(item => item.TabN, exp => exp.Ignore())
                .ForMember(item => item.UserName, exp => exp.Ignore())
                .ForMember(item => item.UserPatronymic, exp => exp.Ignore())
                .ForMember(item => item.UserPatronymic, exp => exp.Ignore())
                .ForMember(item => item.UserSurname, exp => exp.Ignore());

            config.CreateMap<Vacation, VacationOrder>()
                .ForMember(item => item.VacationId, exp => exp.MapFrom(item => item.Id))
                .ForMember(item => item.CreatedDate, exp => exp.Ignore())
                .ForMember(item => item.Id, exp => exp.Ignore())
                .ForMember(item => item.OrderPrinted, exp => exp.Ignore())
                .ForMember(item => item.OrderRecived, exp => exp.Ignore())
                .ForMember(item => item.Vacation, exp => exp.Ignore());


            config.CreateMap<VacationRequestHistoryChange, VacationRequestHistoryChangeViewModel>()
                .ForMember(item => item.VacationRequestHistoryChangeId, exp => exp.MapFrom(item => item.VacationRequestHistoryChangeId))
                .ForMember(item => item.ChangeDate, exp => exp.MapFrom(item => item.ChangeDate))
                .ForMember(item => item.Comment, exp => exp.MapFrom(item => item.Comment))
                .ForMember(item => item.RequestId, exp => exp.MapFrom(item => item.RequestId))
                .ForMember(item => item.State, exp => exp.MapFrom(item => item.State));


            config.CreateMap<Notification, NotificationViewModel>()
                .ForMember(item => item.Id, exp => exp.MapFrom(item => item.Id))
                .ForMember(item => item.EndDate, exp => exp.MapFrom(item => item.Vacation.EndDate))
                .ForMember(item => item.NotificationType, exp => exp.MapFrom(item => item.NotificationType))
                .ForMember(item => item.StartDate, exp => exp.MapFrom(item => item.Vacation.StartDate))
                .ForMember(item => item.UserId, exp => exp.MapFrom(item => item.Vacation.UserProfileId))
                .ForMember(item => item.VacationId, exp => exp.MapFrom(item => item.VacationId))
                .ForMember(item => item.Readed, exp => exp.MapFrom(item => item.Readed))
                .ForMember(item => item.ReadedDate, exp => exp.MapFrom(item => item.ReadedDate))
                .ForMember(item => item.CreatedDate, exp => exp.MapFrom(item => item.CreatedDate))
                .ForMember(item => item.CompanyId, exp => exp.Ignore())
                .ForMember(item => item.CompanyName, exp => exp.Ignore())
                .ForMember(item => item.DepartmentId, exp => exp.Ignore())
                .ForMember(item => item.DepartmentName, exp => exp.Ignore())
                .ForMember(item => item.TabN, exp => exp.Ignore())
                .ForMember(item => item.UserName, exp => exp.Ignore())
                .ForMember(item => item.UserPatronymic, exp => exp.Ignore())
                .ForMember(item => item.UserSurname, exp => exp.Ignore());

            config.CreateMap<Vacation, Notification>()
                .ForMember(item => item.VacationId, exp => exp.MapFrom(item => item.Id))
                .ForMember(item => item.CreatedDate, exp => exp.Ignore())
                .ForMember(item => item.ReadedDate, exp => exp.Ignore())
                .ForMember(item => item.Readed, exp => exp.Ignore())
                .ForMember(item => item.Id, exp => exp.Ignore())
                .ForMember(item => item.Vacation, exp => exp.Ignore())
                .ForMember(item => item.NotificationType, exp => exp.Ignore());

            config.CreateMap<HrRequest, HrRequestModel>();

            config.CreateMap<VacationRequest, VacationRequestViewModel>()
                .ForMember(item => item.Comment, exp => exp.MapFrom(item => item.Comment))
                .ForMember(item => item.CompanyId, exp => exp.MapFrom(item => item.CompanyId))
                .ForMember(item => item.Days, exp => exp.MapFrom(item => item.Days))
                .ForMember(item => item.CreatedDate, exp => exp.MapFrom(item => item.CreatedDate))
                .ForMember(item => item.ChangeDate, exp => exp.MapFrom(item => item.ChangeDate))
                .ForMember(item => item.EndDate, exp => exp.MapFrom(item => item.EndDate))
                .ForMember(item => item.Id, exp => exp.MapFrom(item => item.Id))
                .ForMember(item => item.StartDate, exp => exp.MapFrom(item => item.StartDate))
                .ForMember(item => item.UserProfileId, exp => exp.MapFrom(item => item.UserProfileId))
                .ForMember(item => item.VacationRequestState, exp => exp.MapFrom(item => item.VacationRequestState))
                .ForMember(item => item.VacationType, exp => exp.MapFrom(item => item.VacationType))
                .ForMember(item => item.RequestType, exp => exp.MapFrom(item => item.RequestType))
                .ForMember(item => item.ChangeVacationId, exp => exp.MapFrom(item => item.ChangeVacationId))
                .ForMember(item => item.AnswerComment, exp => exp.MapFrom(item => item.AnswerComment))
                .ForMember(item => item.TabN, exp => exp.Ignore())
                .ForMember(item => item.UserName, exp => exp.Ignore())
                .ForMember(item => item.UserSurname, exp => exp.Ignore())
                .ForMember(item => item.UserPatronymic, exp => exp.Ignore())
                .ForMember(item => item.ChiefFullName, exp => exp.Ignore())
                .ForMember(item => item.ChiefId, exp => exp.Ignore())
                .ReverseMap();

            config.CreateMap<VacationRequest, Vacation>()
                .ForMember(item => item.CompanyId, exp => exp.MapFrom(item => item.CompanyId))
                .ForMember(item => item.Days, exp => exp.MapFrom(item => item.Days))
                .ForMember(item => item.EndDate, exp => exp.MapFrom(item => item.EndDate))
                .ForMember(item => item.StartDate, exp => exp.MapFrom(item => item.StartDate))
                .ForMember(item => item.UserProfileId, exp => exp.MapFrom(item => item.UserProfileId))
                .ForMember(item => item.VacationType, exp => exp.MapFrom(item => item.VacationType))
                .ForMember(item => item.Id, exp => exp.Ignore())
                .ForMember(item => item.CreatedDate, exp => exp.Ignore());





            config.CreateMap<Vacation, VacationViewModel>()
                .ForMember(item => item.CompanyId, exp => exp.MapFrom(item => item.CompanyId))
                .ForMember(item => item.Days, exp => exp.MapFrom(item => item.Days))
                .ForMember(item => item.CreatedDate, exp => exp.MapFrom(item => item.CreatedDate))
                .ForMember(item => item.EndDate, exp => exp.MapFrom(item => item.EndDate))
                .ForMember(item => item.Id, exp => exp.MapFrom(item => item.Id))
                .ForMember(item => item.StartDate, exp => exp.MapFrom(item => item.StartDate))
                .ForMember(item => item.VacationType, exp => exp.MapFrom(item => item.VacationType))
                .ForMember(item => item.TabN, exp => exp.Ignore())
                .ForMember(item => item.UserName, exp => exp.Ignore())
                .ForMember(item => item.UserSurname, exp => exp.Ignore())
                .ForMember(item => item.UserPatronymic, exp => exp.Ignore())
                .ForMember(item => item.ChiefFullName, exp => exp.Ignore())
                .ForMember(item => item.IsFactVaction, exp => exp.Ignore())
                .ReverseMap();

            config.CreateMap<ExternalBalanceVacationImportModel, BalanceVacation>()
                .ForMember(dest => dest.BalanceOfVacation, opt => opt.MapFrom(ext => ext.BalanceOfVacation))
                .ForMember(dest => dest.BalanceId, opt => opt.Ignore())
                .ForMember(dest => dest.UserProfileId, opt => opt.Ignore());

            config.CreateMap<ExternalVacationImportModel, Vacation>()
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(ext => ext.ShiftedStart != null ? ext.ShiftedStartDate : ext.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(ext => ext.ShiftedEnd != null ? ext.ShiftedEndDate : ext.EndDate))
                .ForMember(dest => dest.Days, opt => opt.MapFrom(ext => ext.ShiftedDays > 0 ? ext.ShiftedDays : ext.Days))
                .ForMember(dest => dest.CompanyId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserProfileId, opt => opt.Ignore())
                .ForMember(dest => dest.IsPayment, opt => opt.Ignore())
                .ForMember(dest => dest.VacationType, opt => opt.Ignore());

            config.CreateMap<ExternalFactVacationImportModel, FactVacation>()
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(ext => DateTime.Parse(ext.ScheduleStart)))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(ext => DateTime.Parse(ext.ScheduleEnd)))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserProfileId, opt => opt.Ignore());


        }
    }
}
