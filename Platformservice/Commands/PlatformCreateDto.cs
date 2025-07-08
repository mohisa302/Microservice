using System.ComponentModel.DataAnnotations;
using MediatR;
using PlatformService.Dtos;

namespace PlatformService.Commands
{
  public class PlatformCreateCommand: IRequest<PlatformReadDto>
  {

    public required string Name { get; set; }
    
    public required string Publisher { get; set; }

    public required string Cost { get; set; }

  }
}
