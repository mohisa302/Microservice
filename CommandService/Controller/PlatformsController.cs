using Microsoft.AspNetCore.Mvc;

namespace commandsService.Controllers
{
  [Route("api/c/[controller]")]
  [ApiController]
  public class PlatformsController : ControllerBase
  {
    public PlatformsController()
    {

    }
    [HttpPost]
    public ActionResult TestInboundConnection()
    {
      Console.WriteLine("--> Post");
      return Ok("Inbound test of from Platforms Controller");
    }
  }
  
}
