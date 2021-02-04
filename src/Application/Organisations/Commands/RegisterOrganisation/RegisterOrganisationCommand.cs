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

namespace Application.Organisations.Commands.RegisterOrganisation
{
   public class RegisterOrganisationCommand : IRequest<Result>
    {
        public string OrganisationName { get; set; }
        public string TIN { get; set; }
        public string RCNumber { get; set; }
        public string BVN { get; set; }
        public Guid OrganisationTypeId { get; set; }
        // User profile
        public string Surname { get; set; }
        public string Firstname { get; set; }
        public string Othernames { get; set; }
        public string PhoneNumber { get; set; }
        public int GenderId { get; set; }

    }

    public class RegisterOrganisationCommandHandler : IRequestHandler<RegisterOrganisationCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _user;
        private readonly ILogger<RegisterOrganisationCommandHandler> _logger;

        public RegisterOrganisationCommandHandler(IApplicationDbContext context, ICurrentUserService user, ILogger<RegisterOrganisationCommandHandler> logger)
        {
            _context = context;
            _user = user;
            _logger = logger;
        }

        public async Task<Result> Handle(RegisterOrganisationCommand request, CancellationToken cancellationToken)
        {
            var user = _user.GetUserId();
            if (string.IsNullOrEmpty(user))
            {
                var e = new Exception("Must be logged in");
                _logger.LogError(e.Message);
                return Result.Failure(e.Message);
            }

            Guid userGuid = new Guid(user);
            var userProfiile = await _context.UserProfiles.FirstOrDefaultAsync(x => x.UserId == userGuid);
            if (null != userProfiile)
            {
                var ex = new Exception("User exist");
                _logger.LogError(ex.Message);
                return Result.Failure(ex.Message);
            }

            // Create the user's organisation
            Organisation organisation = new Organisation
            {
                Name = request.OrganisationName,
                BVN = request.BVN,
                IsActive = true,
                IsUnderReview = true,
                OrganisationTypeId = request.OrganisationTypeId,
                RCNumber = request.RCNumber,
                TIN = request.TIN
            };

            userProfiile = new UserProfile
            {
                Surname = request.Surname,
                Firstname = request.Firstname,
                Othernames = request.Othernames,
                Email = _user.GetUserName(),
                GenderId = request.GenderId,
                PhoneNumber = request.PhoneNumber,
                IsActive = true,
                UserId = userGuid,
                Organisation = organisation
            };


            _context.UserProfiles.Add(userProfiile);

            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Organisation {0} created", organisation.Name);

            return Result.Success("Profile created!");
        }
    }
}
