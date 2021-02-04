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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Facade.Applications.Queries
{
    public class GetApplicationTypeListQuery : IRequest<Result>
    {
    }

    public class GetApplicationTypeListQueryHandler : IRequestHandler<GetApplicationTypeListQuery, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserProfile _currentUser;
        private readonly ILogger<GetApplicationTypeListQueryHandler> _logger;

        public GetApplicationTypeListQueryHandler(IApplicationDbContext context, ICurrentUserProfile currentUser, ILogger<GetApplicationTypeListQueryHandler> logger)
        {
            _context = context;
            _currentUser = currentUser;
            _logger = logger;
        }

        public async Task<Result> Handle(GetApplicationTypeListQuery request, CancellationToken cancellationToken)
        {
            var organisation = await _currentUser.GetOrganisation();
            if (null == organisation)
            {
                var e = new NotFoundException(nameof(organisation));
                _logger.LogError(e.Message);
                return Result.Failure("User organisation not found!");
            }

            var list = organisation.OrganisationType.ApplicationTypes;

            return Result.Success(list);
        }
    }
}
