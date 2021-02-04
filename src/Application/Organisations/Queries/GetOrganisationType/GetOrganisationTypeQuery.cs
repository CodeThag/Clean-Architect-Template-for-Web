using Application.Common.Exceptions;
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
    public class GetOrganisationTypeQuery : IRequest<OrganisationType>
    {
        public string Name { get; set; }
    }

    public class GetOrganisationTypeQueryHandler : IRequestHandler<GetOrganisationTypeQuery, OrganisationType>
    {
        private readonly IApplicationDbContext _context;

        public GetOrganisationTypeQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OrganisationType> Handle(GetOrganisationTypeQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.OrganisationTypes.FirstOrDefaultAsync(x => x.Name == request.Name);

            if (null == entity)
                throw new NotFoundException(nameof(entity), request.Name);

            return entity;
        }
    }
}
