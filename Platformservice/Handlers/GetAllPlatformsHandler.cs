using AutoMapper;
using MediatR;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Queries;

public class GetAllPlatformsHandler : IRequestHandler<GetAllPlatformsQuery, IEnumerable<PlatformReadDto>>
{
    private readonly IPlatformRepo _repository;
    private readonly IMapper _mapper;

    public GetAllPlatformsHandler(IPlatformRepo repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PlatformReadDto>> Handle(GetAllPlatformsQuery request, CancellationToken cancellationToken)
    {
        var platformItems = _repository.GetAllPlatforms();
        return _mapper.Map<IEnumerable<PlatformReadDto>>(platformItems);
    }
}
