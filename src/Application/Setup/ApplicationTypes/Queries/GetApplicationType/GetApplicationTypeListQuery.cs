using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Setup.ApplicationTypes.Queries.GetApplicationType
{
    public class GetApplicationTypeListQuery : IRequest<List<ApplicationType>>
    {
    }

    public class GetApplicationTypeListQueryHandler : IRequestHandler<GetApplicationTypeListQuery, List<ApplicationType>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<GetApplicationTypeListQueryHandler> _logger;

        public GetApplicationTypeListQueryHandler(IApplicationDbContext context, ILogger<GetApplicationTypeListQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<ApplicationType>> Handle(GetApplicationTypeListQuery request, CancellationToken cancellationToken)
        {
            return await _context.ApplicationTypes.ToListAsync(cancellationToken);
        }
    }
}
