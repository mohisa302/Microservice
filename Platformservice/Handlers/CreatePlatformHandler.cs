
using AutoMapper;
using MediatR;
using PlatformService.Commands;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

public class CreatePlatformIdHandler : IRequestHandler<PlatformCreateCommand, PlatformReadDto>
{
    private readonly IPlatformRepo _repository;
    private readonly IMapper _mapper;

    public CreatePlatformIdHandler(IPlatformRepo repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }


  public async Task  <PlatformReadDto> Handle(PlatformCreateCommand request, CancellationToken cancellationToken)
  {
    var platformModel = _mapper.Map<Platform>(request);
    _repository.CreatePlatform(platformModel);
    _repository.SaveChanges();
    return  _mapper.Map<PlatformReadDto>(platformModel);
  }
}

