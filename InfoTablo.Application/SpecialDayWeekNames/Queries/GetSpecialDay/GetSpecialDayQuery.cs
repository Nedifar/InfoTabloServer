using MediatR;

namespace InfoTablo.Application.SpecialDayWeekNames.Queries.GetSpecialDay
{
    public record GetSpecialDayQuery() : IRequest<GetSpecialDayVm>;
}
