using Application.DTO;
using Application.VM;
using AutoMapper;
using Domain;

namespace Application.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            string currentUsername = null;
            CreateMap<Activity, Activity>().ReverseMap();
            CreateMap<Activity, ActivityVm>()
                .ForMember(vm => vm.HostUsername, a => a.MapFrom(a => a.Attendees.FirstOrDefault(x => x.IsHost).AppUser.UserName))
                .ForMember(vm => vm.Profiles, a => a.MapFrom(a => a.Attendees));

            CreateMap<ActivityAttendee, VM.ProfileVm>()
                 .ForMember(vm => vm.DisplayName, a => a.MapFrom(a => a.AppUser.DisplayName))
                 .ForMember(vm => vm.Bio, a => a.MapFrom(a => a.AppUser.Bio))
                 .ForMember(vm => vm.Username, a => a.MapFrom(a => a.AppUser.UserName))
                 .ForMember(d => d.FollowersCount, o => o.MapFrom(x => x.AppUser.Followers.Count))
                 .ForMember(d => d.FollowingCount, o => o.MapFrom(x => x.AppUser.Followings.Count))
                 .ForMember(d => d.Following, o => o.MapFrom(s => s.AppUser.Followers.Any(x => x.Observer.UserName == currentUsername)));

            CreateMap<AppUser, VM.ProfileVm>()
                 .ForMember(d => d.Image, o => o.MapFrom(x => x.Photos.FirstOrDefault(x => x.IsMain).Url))
                 .ForMember(d => d.FollowersCount, o => o.MapFrom(x => x.Followers.Count))
                 .ForMember(d => d.FollowingCount, o => o.MapFrom(x => x.Followings.Count))
                 .ForMember(d => d.Following, o => o.MapFrom(s => s.Followers.Any(x => x.Observer.UserName == currentUsername)));

            CreateMap<Comment, CommentDto>()
                 .ForMember(vm => vm.DisplayName, a => a.MapFrom(a => a.Authot.DisplayName))
                 .ForMember(vm => vm.Username, a => a.MapFrom(a => a.Authot.UserName))
                 .ForMember(vm => vm.Username, a => a.MapFrom(a => a.Authot.Photos.FirstOrDefault(X => X.IsMain).Url));



        }
    }
}
