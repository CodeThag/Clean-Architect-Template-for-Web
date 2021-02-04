using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Utilities.Genders.Queries.GetGender
{
    public class GetGenderListQuery : IRequest<List<Gender>>
    {
    }

    public class GetGenderListQueryHandler : IRequestHandler<GetGenderListQuery, List<Gender>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<GetGenderListQueryHandler> _logger;

        public GetGenderListQueryHandler(IApplicationDbContext context, ILogger<GetGenderListQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Gender>> Handle(GetGenderListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Genders.ToListAsync();
        }
    }
}
