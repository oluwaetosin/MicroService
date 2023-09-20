using CommandService.Models;

namespace CommandService.Data
{

    public interface ICommandRepo
    {
        bool SaveChanges();

        IEnumerable<Platform> GetAllPlatforms();
        void CreatePlatform(Platform platform);

        bool PlatformExists(int platformId);

        bool ExternalPlatformExists(int externalPlatformId);

        // COMMANDS

        IEnumerable<Command> GetCommandsForPlatform(int platformId);
        Command GetCommand(int platformId, int CommandId);
        void CreateCommand(int PlatformId, Command command);
        


    }
    
}