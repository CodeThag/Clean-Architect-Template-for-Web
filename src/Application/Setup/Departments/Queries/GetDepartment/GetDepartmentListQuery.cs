using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Setup.Departments.Queries.GetDepartment
{
    public class GetDepartmentListQuery : IRequest<Result>
    {
    }

    public class GetDepartmentListQueryHandler : IRequestHandler<GetDepartmentListQuery , Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<GetDepartmentListQueryHandler> _logger;

        public GetDepartmentListQueryHandler(IApplicationDbContext context, ILogger<GetDepartmentListQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Result> Handle(GetDepartmentListQuery request, CancellationToken cancellationToken)
        {
            var departments = await _context.Departments.ToListAsync(cancellationToken);

            return Result.Success("Success", departments);
        }
    }

}
