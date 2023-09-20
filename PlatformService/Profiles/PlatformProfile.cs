using AutoMapper;
using PlatformService.DTOs;
using PlatformService.Models;

namespace PlatformService.Profiles
{
    class PlatformsProfile : Profile
    {
        public PlatformsProfile()
        {
            // source -> target

            CreateMap<Platform, PlatformReadDto>();
            
            CreateMap<PlatformCreateDto, Platform>();

            CreateMap<PlatformReadDto, PlatformPublishedDto>();
        }
        
    }
}