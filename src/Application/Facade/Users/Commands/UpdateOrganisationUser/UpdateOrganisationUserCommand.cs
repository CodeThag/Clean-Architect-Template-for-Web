using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Facade.Users.Commands.UpdateOrganisationUser
{
    public class UpdateOrganisationUserCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public string Surname { get; set; }
        public string Firstname { get; set; }
        public string Othernames { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int GenderId { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }

    public class UpdateOrganisationUserCommandHandler : IRequestHandler<UpdateOrganisationUserCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<UpdateOrganisationUserCommandHandler> _logger;
        private readonly ICurrentUserProfile _user;

        public UpdateOrganisationUserCommandHandler(IApplicationDbContext context, ILogger<UpdateOrganisationUserCommandHandler> logger, ICurrentUserProfile user)
        {
            _context = context;
            _logger = logger;
            _user = user;
        }

        public async Task<Result> Handle(UpdateOrganisationUserCommand request, CancellationToken cancellationToken)
        {
            Organisation organisation = await _user.GetOrganisation();
            if (null == organisation)
            {
                var e = new Exception("User organisation not set!");
                _logger.LogError(e.Message);
                return Result.Failure(e.Message);
            }

            UserProfile userProfile = organisation.UserProfiles.FirstOrDefault(x => x.Id == request.Id);
            if(null == userProfile)
            {
                var e = new Exception("User profile not found!");
                _logger.LogError(e.Message);
                return Result.Failure(e.Message);
            }

            userProfile.Surname = request.Surname;
            userProfile.Firstname = request.Firstname;
            userProfile.Othernames = request.Othernames;
            userProfile.PhoneNumber = request.PhoneNumber;
            userProfile.GenderId = request.GenderId;
            userProfile.DateOfBirth = request.DateOfBirth;

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success("User profile updated!", userProfile);
        }
    }

    public class UpdateOrganisationUserCommandValidator  : AbstractValidator<UpdateOrganisationUserCommand>
    {
        public UpdateOrganisationUserCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Surname is required");
            RuleFor(x => x.Firstname).NotEmpty().WithMessage("Firstname is required");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email address is required.")
                .EmailAddress().WithMessage("A valid email address is required.");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone number is required"); 
            RuleFor(x => x.GenderId).NotEmpty().WithMessage("Gender is required");

        }
    }
}
