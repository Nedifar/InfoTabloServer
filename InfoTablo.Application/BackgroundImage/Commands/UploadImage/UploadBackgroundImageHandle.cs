using InfoTablo.Application.Interfaces;
using InfoTablo.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoTablo.Application.BackgroundImage.Commands.UploadImage
{
    public class UploadBackgroundImageHandle : IRequestHandler<UploadBackgroundImageCommand>
    {
        private readonly IInfoTabloDbContext _dbContext;

        public UploadBackgroundImageHandle(IInfoTabloDbContext infoTabloDbContext) =>
            _dbContext = infoTabloDbContext;

        public async Task Handle(UploadBackgroundImageCommand request, CancellationToken cancellationToken)
        {
            var extension = request.UploadFile.FileName.Split('.').LastOrDefault();
            string path = AppDomain.CurrentDomain.BaseDirectory + @"background\";

            if (!request.Special)
            {
                using var fileStream = new FileStream(path + "main." + extension, FileMode.Create);
                await request.UploadFile.CopyToAsync(fileStream, cancellationToken);

                _dbContext.SpecialBackgroundPhotos.FirstOrDefault(p => p.TargetDate == null)
                    .FileName = "main." + extension;
            }
            else
            {
                using var fileStream = new FileStream(path + "special" + request.DateTarget.ToShortDateString() + "." + extension, FileMode.Create);
                await request.UploadFile.CopyToAsync(fileStream, cancellationToken);
                var currentModel = _dbContext.SpecialBackgroundPhotos.ToList()
                    .FirstOrDefault(p => p.TargetDate.HasValue
                        && p.TargetDate.Value.ToString("dd.MM.yyyy") == request.DateTarget.ToString("dd.MM.yyyy"));

                if (currentModel == null)
                {
                    currentModel = new SpecialBackgroundPhoto
                    {
                        FileName = "special" + request.DateTarget.ToShortDateString() + "." + extension,
                        TargetDate = request.DateTarget.Date
                    };
                    await _dbContext.SpecialBackgroundPhotos.AddAsync(currentModel, cancellationToken);
                }
                else
                {
                    currentModel.FileName = "special" + request.DateTarget.ToShortDateString() + "." + extension;
                }
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
