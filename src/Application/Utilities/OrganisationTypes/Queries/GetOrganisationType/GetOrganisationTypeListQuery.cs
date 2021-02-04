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

namespace Application.Utilities.OrganisationTypes.Queries.GetOrganisationType
{
    public class GetOrganisationTypeListQuery : IRequest<List<OrganisationType>>
    {
    }

    public class GetOrganisationTypeListQueryHandler : IRequestHandler<GetOrganisationTypeListQuery, List<OrganisationType>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<GetOrganisationTypeListQueryHandler> _logger;

        public GetOrganisationTypeListQueryHandler(IApplicationDbContext context, ILogger<GetOrganisationTypeListQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<OrganisationType>> Handle(GetOrganisationTypeListQuery request, CancellationToken cancellationToken)
        {
            return await _context.OrganisationTypes.ToListAsync();
        }
    }
}
