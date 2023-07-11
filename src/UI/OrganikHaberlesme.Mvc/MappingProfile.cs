using AutoMapper;

using OrganikHaberlesme.Mvc.Models.Employee;
using OrganikHaberlesme.Mvc.Models.LeaveRequest;
using OrganikHaberlesme.Mvc.Models.LeaveType;
using OrganikHaberlesme.Mvc.Models.User;
using OrganikHaberlesme.Mvc.Services.Base;

namespace OrganikHaberlesme.Mvc
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateLeaveTypeDto, CreateLeaveTypeVm>().ReverseMap();
            CreateMap<CreateLeaveRequestDto, CreateLeaveRequestVm>().ReverseMap();
            CreateMap<LeaveRequestDto, LeaveRequestVm>()
                .ForMember(x => x.DateRequested,
                    opt => opt.MapFrom(x => x.DateRequested.DateTime))
                .ForMember(x => x.StartDate,
                    opt => opt.MapFrom(x => x.StartDate.DateTime))
                .ForMember(x => x.EndDate,
                    opt => opt.MapFrom(x => x.EndDate.DateTime))
                .ReverseMap();
            CreateMap<LeaveRequestListDto, LeaveRequestVm>()
                .ForMember(x => x.DateRequested,
                    opt => opt.MapFrom(x => x.DateRequested.DateTime))
                .ForMember(x => x.StartDate,
                    opt => opt.MapFrom(x => x.StartDate.DateTime))
                .ForMember(x => x.EndDate,
                    opt => opt.MapFrom(x => x.EndDate.DateTime))
                .ReverseMap();
            CreateMap<LeaveTypeDto, LeaveTypeVm>().ReverseMap();
            CreateMap<RegisterVm, RegistrationRequest>().ReverseMap();
            CreateMap<EmployeeVm, Employee>();
        }
    }
}

