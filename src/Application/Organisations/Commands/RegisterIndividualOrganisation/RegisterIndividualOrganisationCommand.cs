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

namespace Application.Organisations.Commands.RegisterIndividualOrganisation
{
    public class RegisterIndividualOrganisationCommand : IRequest<Result>
    {
        public string Surname { get; set; }
        public string Firstname { get; set; }
        public string Othernames { get; set; }
        public string PhoneNumber { get; set; }
        public int GenderId { get; set; }
    }
    public class RegisterIndividualOrganisationCommandHandler : IRequestHandler<RegisterIndividualOrganisationCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<RegisterIndividualOrganisationCommandHandler> _logger;
        private readonly ICurrentUserService _user;

        public RegisterIndividualOrganisationCommandHandler(IApplicationDbContext context, ILogger<RegisterIndividualOrganisationCommandHandler> logger, ICurrentUserService user)
        {
            _context = context;
            _logger = logger;
            _user = user;
        }

        public async Task<Result> Handle(RegisterIndividualOrganisationCommand request, CancellationToken cancellationToken)
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
            if(null != userProfiile)
            {
                var ex = new Exception("User exist");
                _logger.LogError(ex.Message);
                return Result.Failure(ex.Message);
            }

            var orgType = await _context.OrganisationTypes.FirstOrDefaultAsync(x => x.Name == "Individual");
            if(null == orgType)
            {
                var typeException = new Exception("Individual organisation type does not exist!");
                _logger.LogError(typeException.Message);
                return Result.Failure(typeException.Message);
            }

            Organisation organisation = new Organisation
            {
                OrganisationType = orgType,
                Name = "Individual Organisation",
                IsActive = true,
                IsUnderReview = true
                
            };

            userProfiile = new UserProfile
            {
                Surname = request.Surname,
                Firstname = request.Firstname,
                Othernames = request.Othernames,
                Email = _user.GetUserName(),
                GenderId  = request.GenderId,
                PhoneNumber = request.PhoneNumber,
                IsActive =  true, 
                UserId = userGuid,
                Organisation = organisation 
            };

            _context.UserProfiles.Add(userProfiile);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success("Profile created!");
        }
    }
}
