using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Setup.MenuResource.Queries.GetMenuItem
{
    public class GetMenuItemListQuery : IRequest<Result>
    {
        public int MenuCollectionId { get; set; }
    }

    public class GetMenuItemListQueryHandler : IRequestHandler<GetMenuItemListQuery, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<GetMenuItemListQueryHandler> _logger;

        public GetMenuItemListQueryHandler(IApplicationDbContext context, ILogger<GetMenuItemListQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Result> Handle(GetMenuItemListQuery request, CancellationToken cancellationToken)
        {
            var collection = await _context.MenuCollections.FindAsync(request.MenuCollectionId);
            if (null == collection)
            {
                var e = new NotFoundException(nameof(collection), request.MenuCollectionId);
                _logger.LogError(e.Message);
                return Result.Failure("Collection not found!");
            }

            var list = collection.MenuItems.ToList();

            return Result.Success("Success", list);
        }
    }
}