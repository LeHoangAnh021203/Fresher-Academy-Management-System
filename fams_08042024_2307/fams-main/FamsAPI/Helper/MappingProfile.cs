using AutoMapper;
using DataLayer.Entities;
using FamsAPI.ViewModel;
using System.Globalization;

namespace FamsAPI.Helper
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<UserPermissionViewModel, UserPermission>();
            CreateMap< UserPermission, UserPermissionViewModel> ();

            CreateMap<User, UserViewModel>().ReverseMap();

            CreateMap<ClassUser, ClassUserViewModel>();
            CreateMap<ClassUserViewModel, ClassUser>();

            CreateMap<TrainingCalendar, TrainingCalendarViewModel>()
                 .ForMember(dest => dest.ClassCode, opt => opt.MapFrom(src => src.Class.ClassCode))
                 .ForMember(dest => dest.LocationId, opt => opt.MapFrom(src => src.Class.LocationId))
                 .ForMember(dest => dest.TrainingProgramCode, opt => opt.MapFrom(src => src.Class.TrainingProgramCode));
            CreateMap<TrainingCalendarViewModel, TrainingCalendar>();

            CreateMap<TrainingCalendar, InputTrainingCalendarViewModel>();
            CreateMap<InputTrainingCalendarViewModel, TrainingCalendar>();   

            CreateMap<Class, ClassViewModel>()
                .ForMember(dest => dest.ClassUsers, opt => opt.MapFrom(src => src.ClassUsers))
                .ForMember(dest => dest.TrainingCalendars, opt => opt.Ignore());
            CreateMap<ClassViewModel, Class>()
                .ForMember(dest => dest.ClassName, opt => opt.Condition(src => src.ClassName != null))
                .ForMember(dest => dest.ClassCode, opt => opt.Condition(src => src.ClassCode != null))
                .ForMember(dest => dest.CreatedBy, opt => opt.Condition(src => src.CreatedBy != null))
                .ForMember(dest => dest.ClassUsers, opt => opt.MapFrom(src => src.ClassUsers));


            CreateMap<InputClassViewModel, Class>()
                .ForMember(dest => dest.ClassName, opt => opt.Condition(src => src.ClassName != null))
                .ForMember(dest => dest.ClassCode, opt => opt.Condition(src => src.ClassCode != null))
                .ForMember(dest => dest.CreatedBy, opt => opt.Condition(src => src.CreatedBy != null));

            CreateMap<Class, InputClassViewModel>();

            CreateMap<TrainingProgram, TrainingProgramViewModel>().ReverseMap();

            CreateMap<TrainingUnit, TrainingUnitViewModel>()
                .ForMember(dest => dest.TrainingContents, opt => opt.MapFrom(src => src.TrainingContents));

            CreateMap<TrainingUnitViewModel, TrainingUnit>()
                .ForMember(dest => dest.TrainingContents, opt => opt.MapFrom(src => src.TrainingContents));
        }
    }
}
