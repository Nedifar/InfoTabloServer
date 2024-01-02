using MediatR;

namespace InfoTablo.Application.Announcments.Queries.GetAnnouncments
{
    public record GetAnnouncmentsQuery() : IRequest<AnnouncmentsVm>;
}
