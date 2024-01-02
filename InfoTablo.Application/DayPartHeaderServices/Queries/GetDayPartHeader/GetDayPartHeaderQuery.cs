using MediatR;

namespace InfoTablo.Application.DayPartHeaderServices.Queries.GetDayPartHeader
{
    public record GetDayPartHeaderQuery() : IRequest<string>;
}
