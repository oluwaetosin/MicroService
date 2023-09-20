using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using CommandService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers
{
    [Route("api/c/platforms/{platformId}/[controller]")]
    public class CommandsController : ControllerBase
    {
        private ICommandRepo _repo;
        private IMapper _mapper;

        public CommandsController(ICommandRepo repo, IMapper mapper)
        {
            _repo = repo;

            _mapper = mapper;
            
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetCommandsForPlatform(int platformId)
        {
            Console.WriteLine($"---> Hit GetCommandsForPlatform: {platformId}");
            
            if(!_repo.PlatformExists(platformId))
            {
                return NotFound();
            }

            var commands = _repo.GetCommandsForPlatform(platformId);

            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
        }

        
        [HttpGet("{commandId}", Name = "GetCommandForPlatform")]
        public ActionResult<IEnumerable<CommandReadDto>> GetCommandForPlatform(int platformId, int commandId)
        {
            Console.WriteLine($"---> Hit GetCommandForPlatform: {platformId} {commandId}");
            
            if(!_repo.PlatformExists(platformId))
            {
                return NotFound();
            }

            var command = _repo.GetCommand(platformId, commandId);

            if(command == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CommandReadDto>(command));

            
        }


        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommandForPlatform(int platformId, [FromBody] CommandCreateDto commanddto)
        {
            Console.WriteLine($"---> Hit CreateCommandForPlatform: {platformId}");
            
            if(!_repo.PlatformExists(platformId))
            {
                return NotFound();
            }
            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(commanddto));

            var command = _mapper.Map<Command>(commanddto);

            Console.WriteLine("---------------inside CreateCommandForPlatform");

            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(command));

             Console.WriteLine("---------------inside CreateCommandForPlatform end");

            _repo.CreateCommand(platformId, command);

            _repo.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(command);

            return CreatedAtRoute(nameof(GetCommandForPlatform),
                new {platformId = platformId, commandId = commandReadDto.Id }, commandReadDto);
           

            
        }
    }
}