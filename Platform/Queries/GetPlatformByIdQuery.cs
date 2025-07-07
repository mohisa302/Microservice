using MediatR;
using PlatformService.Dtos;

namespace PlatformService.Queries
{
    public class GetPlatformByIdQuery : IRequest<PlatformReadDto> // Returns one DTO
    {
        public int Id { get; }

        public GetPlatformByIdQuery(int id)
        {
            Id = id;
        }
    }
}
