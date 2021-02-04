using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Setup.ApplicationTypes.Queries.GetApplicationType
{
    public class GetApplicationTypeQuery : IRequest<Result>
    {
        public Guid Id { get; set; }
    }

    public class GetApplicationTypeQueryHandler : IRequestHandler<GetApplicationTypeQuery, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<GetApplicationTypeQueryHandler> _logger;

        public GetApplicationTypeQueryHandler(IApplicationDbContext context, ILogger<GetApplicationTypeQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Result> Handle(GetApplicationTypeQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.ApplicationTypes.FindAsync(request.Id);
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
