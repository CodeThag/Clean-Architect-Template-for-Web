using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Setup.Departments.Queries.GetDepartment
{
    public class GetDepartmentQuery : IRequest<Result>
    {
        public Guid Id { get; set; }
    }

    public class GetDepartmentQueryHandler : IRequestHandler<GetDepartmentQuery, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<GetDepartmentQueryHandler> _logger;

        public GetDepartmentQueryHandler(IApplicationDbContext context, ILogger<GetDepartmentQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Result> Handle(GetDepartmentQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Departments.FindAsync(request.Id);
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