using MediatR;

namespace InfoTablo.Application.Lessons.Queries.GetAllLessons
{
    public record GetAllLessonsQuery() : IRequest<GetAllLessonsVm>;
}
