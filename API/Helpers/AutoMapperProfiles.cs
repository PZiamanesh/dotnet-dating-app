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
                .ForMember(dest => dest.Age, x => x.MapFrom(src => src.DateOfBirth.CalculateAge()))
                .ForMember(desr => desr.PhotoUrl, x => x.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain)!.Url));

            CreateMap<Photo, PhotoDto>();

            CreateMap<MemberUpdateDto, AppUser>();

            CreateMap<RegisterDto, AppUser>();
        }
    }
}
