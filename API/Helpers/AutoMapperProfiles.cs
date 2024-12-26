using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDto>()
                .ForMember(d => d.Age, s => s.MapFrom(x => x.DateOfBirth.CalculateAge()))
                .ForMember(d => d.PhotoUrl, s => s.MapFrom(x => x.Photos.FirstOrDefault(p => p.IsMain)!.Url));

            CreateMap<Photo, PhotoDto>();

            CreateMap<MemberUpdateDto, AppUser>();
        }
    }
}
