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

namespace Application.Facade.Users.Queries.GetOrganisationUsers
{
    public class GetOrganisationUserListQuery : IRequest<Result>
    {
    }

    public class GetOrganisationUserListQueryHandler : IRequestHandler<GetOrganisationUserListQuery, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<GetOrganisationUserListQueryHandler> _logger;
        private readonly ICurrentUserProfile _user;

        public GetOrganisationUserListQueryHandler(IApplicationDbContext context, ILogger<GetOrganisationUserListQueryHandler> logger, ICurrentUserProfile user)
        {
            _context = context;
            _logger = logger;
            _user = user;
        }

        public async Task<Result> Handle(GetOrganisationUserListQuery request, CancellationToken cancellationToken)
        {
            Organisation organisation = await _user.GetOrganisation();
            if(null == organisation)
            {
                var e = new NotFoundException(nameof(organisation));
                _logger.LogError(e.Message);
                return Result.Failure("Organisation not found!");
            }

            List<UserProfile> users = organisation.UserProfiles.ToList();

            return Result.Success("Success", users);
        }
    }
}
