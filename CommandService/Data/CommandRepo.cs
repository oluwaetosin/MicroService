using CommandService.Models;

namespace CommandService.Data
{

    public class CommandRepo : ICommandRepo
    {
        private AppDbContext _context;
        public CommandRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreateCommand(int PlatformId, Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            command.PlatformId = PlatformId;

            _context.Commands.Add(command);
        }

        public void CreatePlatform(Platform platform)
        {
            if(platform == null)
            {
                throw new ArgumentNullException(nameof(platform));
            }

            _context.Platforms.Add(platform);
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
           return  _context.Platforms; 
        }

        public Command GetCommand(int platformId, int CommandId)
        {
            return _context.Commands.Where(x => x.PlatformId == platformId && x.Id == CommandId).FirstOrDefault();
        }

        public IEnumerable<Command> GetCommandsForPlatform(int platformId)
        {
            return _context.Commands
            .Where(x=>x.PlatformId == platformId)
            .OrderBy(c=>c.Platform.Name);
        }

        public bool PlatformExists(int platformId)
        {
           return _context.Platforms.Any(p => p.Id == platformId);
        }

        public bool SaveChanges()
        {
          return  _context.SaveChanges() >= 0;
        }
    }
}