using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Enums;
using Application.Common.Exceptions;
using Application.Common.Helper;
using Application.Common.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Setup.Roles.Commands.RemoveRoleClaim
{
    public class RemoveRoleClaimCommand : IRequest<Result>
    {
        public string RoleId { get; set; }
        public string ClaimValue { get; set; }
    }

    public class RemoveRoleClaimCommandHandler : IRequestHandler<RemoveRoleClaimCommand, Result>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<RemoveRoleClaimCommandHandler> _logger;

        public RemoveRoleClaimCommandHandler(RoleManager<IdentityRole> roleManager,
            ILogger<RemoveRoleClaimCommandHandler> logger)
        {
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task<Result> Handle(RemoveRoleClaimCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(request.RoleId);
            if (null == role)
            {
                var e = new NotFoundException(nameof(role), request.RoleId);
                _logger.LogError(e.Message);
                return Result.Failure("Role not found!");
            }

            var claim = new System.Security.Claims.Claim(ClaimTypes.Permissions.GetAttributeStringValue(),
                request.ClaimValue);
            var result = await _roleManager.RemoveClaimAsync(role, claim);

            if (result.Succeeded)
                return Result.Success("Claim removed!");
            return Result.Failure("Claim not removed");
        }
    }

    public class RemoveRoleClaimCommandValidator: AbstractValidator<RemoveRoleClaimCommand>
    {
        public RemoveRoleClaimCommandValidator()
        {
            RuleFor(x => x.ClaimValue).NotEmpty();
            RuleFor(x => x.RoleId).NotEmpty();
        }
    }
}
