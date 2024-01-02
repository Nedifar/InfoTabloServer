using MediatR;

namespace InfoTablo.Application.TimeShedules.Queries.GetTimeScheduleForDynamicUpdate
{
    public record GetTimeScheduleForDynamicUpdateQuery() : IRequest<GetTimeScheduleForDynamicUpdateVm>;
}
