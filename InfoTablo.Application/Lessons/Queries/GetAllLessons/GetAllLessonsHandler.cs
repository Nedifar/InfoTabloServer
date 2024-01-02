using InfoTablo.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InfoTablo.Application.Lessons.Queries.GetAllLessons
{
    public class GetAllLessonsHandler : IRequestHandler<GetAllLessonsQuery, GetAllLessonsVm>
    {
        private readonly IInfoTabloDbContext _dbContext;

        public GetAllLessonsHandler(IInfoTabloDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetAllLessonsVm> Handle(GetAllLessonsQuery request, CancellationToken cancellationToken)
        {
            var lessons = await _dbContext.Lessons.ToListAsync(cancellationToken);
            
            var resultCollectionContainer = new GetAllLessonsVm { AllLessons = lessons };

            return resultCollectionContainer;
        }
    }
}
