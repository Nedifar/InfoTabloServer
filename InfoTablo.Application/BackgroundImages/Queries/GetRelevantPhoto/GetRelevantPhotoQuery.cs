using MediatR;

namespace InfoTablo.Application.BackgroundImages.Queries.GetRelevantPhoto
{
    public record GetRelevantPhotoQuery() : IRequest<string>;
}
