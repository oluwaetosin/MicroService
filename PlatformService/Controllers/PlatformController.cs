using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.DTOs;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private IPlatformRepo _repository;
        private IMapper _mapper;

        public PlatformsController(IPlatformRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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



    }
}