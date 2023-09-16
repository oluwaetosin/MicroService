using AutoMapper;
using CommandService.Dtos;
using CommandService.Models;

namespace CommandService.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            // source ==> target

            CreateMap<Platform, PlatformReadDto>();   
            CreateMap<CommandCreateDtos, Command>();
            CreateMap<Command, CommandReadDto>();
        }
    }
}