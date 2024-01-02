using InfoTablo.Domain;
using MediatR;

namespace InfoTablo.Application.Paras.Queries.GetParasToday
{
    public record GetParasTodayQuery() : IRequest<IList<Para>>;
}
