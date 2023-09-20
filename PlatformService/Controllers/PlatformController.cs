using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.AsyncDataServices;
using PlatformService.Data;
using PlatformService.DTOs;
using PlatformService.Models;
using PlatformService.SyncDataServices;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private IPlatformRepo _repository;
        private IMapper _mapper;
        private ICommandDataClient _commandDataClient;
        private readonly IMessageBusClient _messageBusClient;

        public PlatformsController(
            IPlatformRepo repository, 
            IMapper mapper,
            ICommandDataClient commandDataClient,
            IMessageBusClient messageBusClient
        )
        {
            _repository = repository;
            _mapper = mapper;
            _commandDataClient = commandDataClient;
            _messageBusClient = messageBusClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatForms()
        {
            var platforms = _repository.GetAllPlatforms();

            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platforms));
        }

        [HttpGet("{id}", Name = "GetPlatFormById")]
        public ActionResult<PlatformReadDto> GetPlatFormById(int id)
        {
            var platform = _repository.GetPlatformById(id);

            if(platform != null)
            {
                return Ok(_mapper.Map<PlatformReadDto>(platform));
            }

            return NotFound();

           
        }

        public  async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto paltformCreateDto)
        {
            var platFormModel = _mapper.Map<Platform>(paltformCreateDto);

            _repository.CreatePlatform(platFormModel);

            _repository.SaveChanges();

            var platformReadDto = _mapper.Map<PlatformReadDto>(platFormModel);

            //send sync message

            try
            {
                await _commandDataClient.SendPlatFormToCommand(platformReadDto);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"----->  Could not send synchronously: {ex.Message}");
            }

            // send async message

            try
            {
                var platformPublishedDto = _mapper.Map<PlatformPublishedDto>(platformReadDto);

                platformPublishedDto.Event = "Platform_Published";

                _messageBusClient.PublishNewPlatform(platformPublishedDto);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"----->  Could not send asynchronously: {ex.Message}");
            }

            return CreatedAtRoute(nameof(GetPlatFormById), new {Id = platformReadDto.Id}, platformReadDto );
        }



    }
}