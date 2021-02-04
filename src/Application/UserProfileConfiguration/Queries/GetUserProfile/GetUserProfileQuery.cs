using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserProfileConfiguation.Queries.GetUserProfile
{
    public class GetUserProfileQuery : IRequest<UserProfile>
    {
    }

    public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, UserProfile>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<GetUserProfileQueryHandler> _logger;
        private readonly ICurrentUserService _user;

        public GetUserProfileQueryHandler(IApplicationDbContext context, ILogger<GetUserProfileQueryHandler> logger, ICurrentUserService user)
        {
            _context = context;
            _logger = logger;
            _user = user;
        }

        public async Task<UserProfile> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            var userId = new Guid(_user.GetUserId());
            UserProfile profile = await _context.UserProfiles.FirstOrDefaultAsync(x => x.UserId == userId);
            if (null == userId)
            {
                var e = new NotFoundException(nameof(profile), userId);
                _logger.LogError(e.Message);
                return null;
            }

            return profile;
        }
    }
}
