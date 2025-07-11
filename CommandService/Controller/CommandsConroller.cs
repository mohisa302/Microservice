using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace commandsService.Controllers
{
  [Route("api/c/platforms/{platformId}/[controller]")]
  [ApiController]
  public class CommandsController : ControllerBase
  {
    private readonly ICommandRepo _repo;
    private readonly IMapper _mapper;

    public CommandsController(
      ICommandRepo repo,
      IMapper mapper
    )
    {
      _repo = repo;
      _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CommandReadDto>> GetAllCommandsForPlatforms(int platId)
    {
      Console.WriteLine("--> Get Commands");
      var commandItems = _repo.GetCommandsForPlatform(platId);
      return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
    }

    [HttpPost]
    public ActionResult TestInboundConnection()
    {
      Console.WriteLine("--> Post");
      return Ok("Inbound test of from Platforms Controller");
    }
  }
  
}
