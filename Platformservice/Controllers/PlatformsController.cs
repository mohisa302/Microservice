using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Commands;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Queries;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PlatformsController : ControllerBase
  {
    public readonly IPlatformRepo _repostory;
    public readonly IMapper _mapper;
    public readonly IMediator _mediator;
    public readonly ICommandDataClient _commandDataClient;

    public PlatformsController(
      IPlatformRepo repository,
      IMapper mapper,
      IMediator mediator,
      ICommandDataClient commandDataClient
      )
    {
      _repostory = repository;
      _mapper = mapper;
      _mediator = mediator;
      _commandDataClient = commandDataClient;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlatformReadDto>>> GetPlatforms()
    {
      Console.WriteLine("--> Getting Platforms...");
      var query = new GetAllPlatformsQuery();
      var result = await _mediator.Send(query);
      return Ok(result);
    }
   
    [HttpGet("{id}", Name = "GetPlatformById")] //Same name as method signature
    public async Task<ActionResult<PlatformReadDto>> GetPlatformById(int id)
    {
      Console.WriteLine("--> Getting Platforms...");
      var query = new GetPlatformByIdQuery(id);
      var result = await _mediator.Send(query);
      return result != null ? Ok(result) : NotFound();
    }

    [HttpPost] //Same name as method signature
    public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateCommand command)
    {
      var result = await _mediator.Send(command);
      try{
        await _commandDataClient.SendPlatformToCommand(result);

      }
      catch(Exception ex)
      {
        Console.WriteLine($"--> {ex.Message}");
      }
      return CreatedAtRoute(nameof(GetPlatformById), new { result.Id }, result);

    }
  }
}
