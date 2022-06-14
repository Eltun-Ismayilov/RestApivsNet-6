using Application.VM;
using AutoMapper;
using Domain;

namespace Application.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Activity, Activity>().ReverseMap();
            CreateMap<Activity, ActivityVm>()
                .ForMember(vm => vm.HostUsername, a => a.MapFrom(a => a.Attendees.FirstOrDefault(x => x.IsHost).AppUser.UserName))
                .ForMember(vm => vm.Profiles, a => a.MapFrom(a => a.Attendees));

            CreateMap<ActivityAttendee, VM.ProfileVm>()
                 .ForMember(vm => vm.DisplayName, a => a.MapFrom(a => a.AppUser.DisplayName))
                  .ForMember(vm => vm.Bio, a => a.MapFrom(a => a.AppUser.Bio))
                 .ForMember(vm => vm.Username, a => a.MapFrom(a => a.AppUser.UserName));

        }
    }
}
