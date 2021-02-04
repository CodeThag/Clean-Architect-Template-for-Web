using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Organisations.Queries.GetOrganisations
{
    public class GetOrganisationQuery : IRequest<Organisation>
    {
        public Guid Id { get; set; }
    }

    public class GetOrganisationQueryHandler : IRequestHandler<GetOrganisationQuery, Organisation>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<GetOrganisationQueryHandler> _logger;

        public GetOrganisationQueryHandler(IApplicationDbContext context, ILogger<GetOrganisationQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Organisation> Handle(GetOrganisationQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Organisations.FindAsync(request.Id);
            if(null == entity)
            {
                var e = new NotFoundException(nameof(entity), request.Id);
                _logger.LogError(e.Message);
                return null;
            }

            return entity;
        }
    }
}
