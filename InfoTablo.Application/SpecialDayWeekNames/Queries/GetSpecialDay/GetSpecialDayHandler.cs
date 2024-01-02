using AutoMapper;
using InfoTablo.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoTablo.Application.SpecialDayWeekNames.Queries.GetSpecialDay
{
    public class GetSpecialDayHandler : IRequestHandler<GetSpecialDayQuery, GetSpecialDayVm>
    {
        private readonly IInfoTabloDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetSpecialDayHandler(IInfoTabloDbContext dbContext, IMapper mapper)
        {
            (_dbContext, _mapper) = (dbContext, mapper);
        }

        public async Task<GetSpecialDayVm> Handle(GetSpecialDayQuery request, CancellationToken cancellationToken)
        {
            var currentSpecialDay = await _dbContext.SpecialDayWeekNames.FirstOrDefaultAsync(cancellationToken);
            var result = _mapper.Map<GetSpecialDayVm>(currentSpecialDay);
            return result;
        }
    }
}
