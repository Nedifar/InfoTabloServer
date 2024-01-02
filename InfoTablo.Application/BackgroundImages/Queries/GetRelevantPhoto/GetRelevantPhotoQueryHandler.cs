using InfoTablo.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InfoTablo.Application.BackgroundImages.Queries.GetRelevantPhoto
{
    public class GetRelevantPhotoQueryHandler : IRequestHandler<GetRelevantPhotoQuery, string>
    {
        private readonly IInfoTabloDbContext _dbContext;

        public GetRelevantPhotoQueryHandler(IInfoTabloDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> Handle(GetRelevantPhotoQuery request, CancellationToken cancellationToken)
        {
            var currentPhoto = await _dbContext.SpecialBackgroundPhotos.FirstOrDefaultAsync(p => p.TargetDate.HasValue
                && p.TargetDate.Value.Date == DateTime.Now.Date, cancellationToken);
            var path = currentPhoto == null
                ? _dbContext.SpecialBackgroundPhotos.FirstOrDefault(p => p.TargetDate.HasValue).FileName
                : currentPhoto.FileName;
            return path;
        }
    }
}
