using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly ICommandRepo _repo;
        private readonly IMapper _mapper;

        public PlatformsController(ICommandRepo repo, IMapper mapper)
        {
            _repo = repo;

            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult TestInblundConnection(){

            Console.WriteLine("------> Inbound Post");

            return Ok("Inbound test ok from Command Platforms controller");
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("---> Getting Platforms from ComandService");

            var platformItems = _repo.GetAllPlatforms();

            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItems));
        }
    }  
}