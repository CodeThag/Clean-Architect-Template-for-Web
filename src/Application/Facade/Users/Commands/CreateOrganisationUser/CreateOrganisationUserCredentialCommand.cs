using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
    public class CreateOrganisationUserCredentialCommand : IRequest<Result>
    {
        public Guid UserId { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class CreateOrganisationUserCredentialCommandHandler : IRequestHandler<CreateOrganisationUserCredentialCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<CreateOrganisationUserCredentialCommandHandler> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IHttpContextAccessor _accessor;
        private readonly LinkGenerator _linkGenerator;

        public CreateOrganisationUserCredentialCommandHandler(IApplicationDbContext context, ILogger<CreateOrganisationUserCredentialCommandHandler> logger,
            UserManager<IdentityUser> userManager, IEmailSender emailSender, IHttpContextAccessor accessor, LinkGenerator linkGenerator)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
            _emailSender = emailSender;
            _accessor = accessor;
            _linkGenerator = linkGenerator;
        }

        public async Task<Result> Handle(CreateOrganisationUserCredentialCommand request, CancellationToken cancellationToken)
        {
            try
            {
                UserProfile profile = await _context.UserProfiles.FindAsync(request.UserId);

                if (null == profile)
                {
                    var e = new NotFoundException(nameof(profile), request.UserId);
                    _logger.LogError(e.Message);
                    return Result.Failure(e.Message);
                }

                if (string.IsNullOrEmpty(request.Password))
                {
                    _logger.LogError("Password is required!");
                    return Result.Failure("Password is required!");
                }

                var user = new IdentityUser { UserName = profile.Email, Email = profile.Email };
                var result = await _userManager.CreateAsync(user, request.Password);

                //    Should be able to send emails or sms here

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));


                var callbackUrl = _linkGenerator.GetUriByPage(
                _accessor.HttpContext,
                    "/Account/ConfirmEmail",
                    handler: null,
                    values: new { area = "Identity", userId = user.Id, code = code });

                // TODO: Change this to use notification services
                await _emailSender.SendEmailAsync(profile.Email, "Confirm your email",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");


                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success("User profile with login credentials created!", profile);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Result.Failure("Oops! Something went wrong!");
            }
        }
    }
}
