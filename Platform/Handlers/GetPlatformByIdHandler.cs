using AutoMapper;
using MediatR;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Queries;

public class GetPlatformByIdHandler : IRequestHandler<GetPlatformByIdQuery, PlatformReadDto>
{
    private readonly IPlatformRepo _repository;
    private readonly IMapper _mapper;

    public GetPlatformByIdHandler(IPlatformRepo repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PlatformReadDto?> Handle(GetPlatformByIdQuery request, CancellationToken cancellationToken)
    {
        var platformItem = _repository.GetPlatformById(request.Id);
        if (platformItem != null)
        {
            return _mapper.Map<PlatformReadDto>(platformItem);
        }
        return null;
    }
}

