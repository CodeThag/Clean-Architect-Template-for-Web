using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Facade.Users.Commands.CreateOrganisationUser
{
    public class CreateOrganisationUserCommand : IRequest<Result>
    {
        public string Surname { get; set; }
        public string Firstname { get; set; }
        public string Othernames { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int GenderId { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }

    public class CreateOrganisationUserCommandHandler : IRequestHandler<CreateOrganisationUserCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<CreateOrganisationUserCommandHandler> _logger;
        private readonly ICurrentUserProfile _user;

        public CreateOrganisationUserCommandHandler(IApplicationDbContext context, ILogger<CreateOrganisationUserCommandHandler> logger,
            ICurrentUserProfile user)
        {
            _context = context;
            _logger = logger;
            _user = user;
        }

        public async Task<Result> Handle(CreateOrganisationUserCommand request, CancellationToken cancellationToken)
        {
            Organisation organisation = await _user.GetOrganisation();
            if (null == organisation)
            {
                var e = new Exception("User organisation not set!");
                _logger.LogError(e.Message);
                return Result.Failure(e.Message);
            }

            UserProfile profile = new UserProfile
            {
                Organisation = organisation,
                Surname = request.Surname,
                Firstname = request.Firstname,
                Othernames = request.Othernames,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                DateOfBirth = request.DateOfBirth,
                GenderId = request.GenderId,
                IsActive = true,
            };

            _context.UserProfiles.Add(profile);

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success("User profile created!", profile);
        }
    }

    public class CreateOrganisationUserCommandValidator : AbstractValidator<CreateOrganisationUserCommand>
    {
        public CreateOrganisationUserCommandValidator()
        {
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Surname is required");
            RuleFor(x => x.Firstname).NotEmpty().WithMessage("Firstname is required");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email address is required.")
                .EmailAddress().WithMessage("A valid email address is required.");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone number is required");
            RuleFor(x => x.GenderId).NotEmpty().WithMessage("Gender is required");
        }
    }
}
