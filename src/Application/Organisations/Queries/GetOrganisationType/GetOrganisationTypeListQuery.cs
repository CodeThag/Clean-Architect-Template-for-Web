using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Organisations.Queries.GetOrganisationType
{
    public class GetOrganisationTypeListQuery : IRequest<List<OrganisationType>>
    {
        public string Name { get; set; }
    }

    public class GetOrganisationTypeListQueryHandler : IRequestHandler<GetOrganisationTypeListQuery, List<OrganisationType>>
    {
        private readonly IApplicationDbContext _context;

        public GetOrganisationTypeListQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrganisationType>> Handle(GetOrganisationTypeListQuery request, CancellationToken cancellationToken)
        {
            return await _context.OrganisationTypes.ToListAsync();
        }
    }
}
