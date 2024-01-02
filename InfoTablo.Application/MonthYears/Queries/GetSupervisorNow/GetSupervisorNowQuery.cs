using MediatR;

namespace InfoTablo.Application.MonthYears.Queries.GetSupervisorNow
{
    public record GetSupervisorNowQuery() : IRequest<string>;
}
