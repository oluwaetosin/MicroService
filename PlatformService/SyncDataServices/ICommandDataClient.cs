using PlatformService.DTOs;

namespace PlatformService.SyncDataServices
{
    public interface ICommandDataClient
    {
        Task SendPlatFormToCommand(PlatformReadDto plat);
        
    }
}