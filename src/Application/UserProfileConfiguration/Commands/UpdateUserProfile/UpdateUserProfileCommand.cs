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

namespace Application.UserProfileConfiguation.Commands.UpdateUserProfile
{
    public class UpdateUserProfileCommand : IRequest<Result>
    {
        public string Surname { get; set; }
        public string Firstname { get; set; }
        public string Othernames { get; set; }
        public string PhoneNumber { get; set; }
        public int GenderId { get; set; }
    }

    public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<UpdateUserProfileCommandHandler> _logger;
        private readonly ICurrentUserService _user;

        public UpdateUserProfileCommandHandler(IApplicationDbContext context, ILogger<UpdateUserProfileCommandHandler> logger, ICurrentUserService user)
        {
            _context = context;
            _logger = logger;
            _user = user;
        }

        public async Task<Result> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var userId = new Guid(_user.GetUserId());
            UserProfile profile = await _context.UserProfiles.FirstOrDefaultAsync(x => x.UserId == userId);
            if(null == userId)
            {
                var e = new NotFoundException(nameof(profile), userId);
                _logger.LogError(e.Message);
                return Result.Failure("User profile not found!");
            }

            profile.Surname = request.Surname;
            profile.Firstname = request.Firstname;
            profile.Othernames = request.Othernames;
            profile.PhoneNumber = request.PhoneNumber;
            profile.GenderId = request.GenderId;

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success("Profile updated", profile);
        }
    }
}
