using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Setup.MenuResource.Queries.GetMenuCollection
{
    public class GetMenuCollectionQuery : IRequest<Result>
    {
        public int Id { get; set; }
    }

    public class GetMenuCollectionQueryHandler : IRequestHandler<GetMenuCollectionQuery, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<GetMenuCollectionListQueryHandler> _logger;

        public GetMenuCollectionQueryHandler(IApplicationDbContext context, ILogger<GetMenuCollectionListQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Result> Handle(GetMenuCollectionQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.MenuCollections.FindAsync(request.Id);
            if (null == entity)
            {
                var e = new NotFoundException(nameof(entity), request.Id);
                _logger.LogError(e.Message);
                return Result.Failure(e.Message);
            }

            return Result.Success("Success", entity);
        }
    }
}
