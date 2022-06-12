using AutoMapper;
using Domain;

namespace Application.AutoMapper
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Activity, Activity>().ReverseMap();
        }
    }
}
