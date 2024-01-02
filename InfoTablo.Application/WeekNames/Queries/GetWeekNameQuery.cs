using MediatR;

namespace InfoTablo.Application.WeekNames.Queries
{
    public record GetWeekNameQuery() : IRequest<string>;
}
