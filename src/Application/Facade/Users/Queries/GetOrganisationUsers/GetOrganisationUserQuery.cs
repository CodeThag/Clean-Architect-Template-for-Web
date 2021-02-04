using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Facade.Users.Queries.GetOrganisationUsers
{
    public class GetOrganisationUserQuery : IRequest<Result>
    {
        public Guid Id { get; set; }
    }

    public class GetOrganisationUserQueryHandler : IRequestHandler<GetOrganisationUserQuery, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<GetOrganisationUserQueryHandler> _logger;

        public GetOrganisationUserQueryHandler(IApplicationDbContext context, ILogger<GetOrganisationUserQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Result> Handle(GetOrganisationUserQuery request, CancellationToken cancellationToken)
        {
            UserProfile profile = await _context.UserProfiles.FindAsync(request.Id);

            if (null == profile)
            {
                var e = new NotFoundException(nameof(profile), request.Id);
                _logger.LogError(e.Message);
                return Result.Failure("User profile not found!");
            }

            return Result.Success("Success", profile);
        }
    }
}
