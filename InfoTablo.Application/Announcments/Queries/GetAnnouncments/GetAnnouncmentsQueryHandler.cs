using InfoTablo.Application.Interfaces;
using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;

namespace InfoTablo.Application.Announcments.Queries.GetAnnouncments
{
    public class GetAnnouncmentsQueryHandler
        : IRequestHandler<GetAnnouncmentsQuery, AnnouncmentsVm>
    {
        private readonly IMapper _mapper;
        private readonly IInfoTabloDbContext _dbConext;

        public GetAnnouncmentsQueryHandler(IMapper mapper, IInfoTabloDbContext dbConext) =>
            (_mapper, _dbConext) = (mapper, dbConext);

        public async Task<AnnouncmentsVm> Handle(GetAnnouncmentsQuery request, CancellationToken cancellationToken)
        {
            var resultList = await _dbConext.Announcments
                .Where(p => p.DateAdded < DateTime.Now
                    && (p.DateClosed >= DateTime.Now || p.DateClosed == null)
                    && p.IsActive)
                .OrderByDescending(p => p.Priority)
                .ThenByDescending(p => p.IdAnnouncement)
                .Take(5)
                .ProjectTo<AnnouncmentLookupDto>(_mapper.ConfigurationProvider).
                ToListAsync(cancellationToken);

            return new AnnouncmentsVm { Announcments = resultList };
        }
    }
}
