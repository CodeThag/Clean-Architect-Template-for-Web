using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Setup.MenuResource.Queries.GetMenuCollection
{
    public class GetMenuCollectionListQuery : IRequest<List<MenuCollection>>
    {
    }

    public class GetMenuCollectionListQueryHandler : IRequestHandler<GetMenuCollectionListQuery, List<MenuCollection>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<GetMenuCollectionListQueryHandler> _logger;

        public GetMenuCollectionListQueryHandler(IApplicationDbContext context, ILogger<GetMenuCollectionListQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<MenuCollection>> Handle(GetMenuCollectionListQuery request, CancellationToken cancellationToken)
        {
            return await _context.MenuCollections.ToListAsync();
        }
    }
}
