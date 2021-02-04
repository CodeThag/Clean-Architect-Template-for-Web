using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserProfileConfiguation.Queries.GetOrganisationProfile
{
    public class GetOrganisationProfileQuery : IRequest<Organisation>
    {
    }

    public class GetOrganisationProfileQueryHandler : IRequestHandler<GetOrganisationProfileQuery, Organisation>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<GetOrganisationProfileQueryHandler> _logger;
        private readonly ICurrentUserService _user;

        public GetOrganisationProfileQueryHandler(IApplicationDbContext context, ILogger<GetOrganisationProfileQueryHandler> logger, ICurrentUserService user)
        {
            _context = context;
            _logger = logger;
            _user = user;
        }

        public async Task<Organisation> Handle(GetOrganisationProfileQuery request, CancellationToken cancellationToken)
        {
            var userId = new Guid(_user.GetUserId());
            UserProfile profile = await _context.UserProfiles.FirstOrDefaultAsync(x => x.UserId == userId);
            if (null == userId)
            {
                var e = new NotFoundException(nameof(profile), userId);
                _logger.LogError(e.Message);
                return null;
            }

            if(null == profile.Organisation)
            {
                var e = new NotFoundException(nameof(profile.Organisation), "For user: " + userId);
                _logger.LogError(e.Message);
                return null;
            }

            return profile.Organisation;
        }
    }
}
