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

namespace Application.UserProfileConfiguation.Commands.UpdateOrganisationProfile
{
    public class UpdateOrganisationProfileCommand : IRequest<Result>
    {
        public string Name { get; set; }
    }

    public class UpdateOrganisationProfileCommandHandler : IRequestHandler<UpdateOrganisationProfileCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<UpdateOrganisationProfileCommandHandler> _logger;
        private readonly ICurrentUserService _user;

        public UpdateOrganisationProfileCommandHandler(IApplicationDbContext context, ILogger<UpdateOrganisationProfileCommandHandler> logger, ICurrentUserService user)
        {
            _context = context;
            _logger = logger;
            _user = user;
        }

        public async Task<Result> Handle(UpdateOrganisationProfileCommand request, CancellationToken cancellationToken)
        {
            var userId = new Guid(_user.GetUserId());
            UserProfile profile = await _context.UserProfiles.FirstOrDefaultAsync(x => x.UserId == userId);
            if (null == userId)
            {
                var e = new NotFoundException(nameof(profile), userId);
                _logger.LogError(e.Message);
                return Result.Failure("User profile not found!");
            }

            Organisation organisation = profile.Organisation;
            organisation.Name = request.Name;

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success("Organisation profile updated!", organisation);
        }
    }
}
