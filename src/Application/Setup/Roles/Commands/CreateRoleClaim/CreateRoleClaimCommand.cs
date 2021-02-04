using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Enums;
using Application.Common.Exceptions;
using Application.Common.Helper;
using Application.Common.Models;
using Application.Setup.Roles.Commands.CreateRole;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Setup.Roles.Commands.CreateRoleClaim
{
    public class CreateRoleClaimCommand : IRequest<Result>
    {
        public string RoleId { get; set; }
        public string ClaimValue { get; set; }
    }

    public class CreateRoleClaimCommandHandler : IRequestHandler<CreateRoleClaimCommand, Result>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<CreateRoleClaimCommandHandler> _logger;

        public CreateRoleClaimCommandHandler(RoleManager<IdentityRole> roleManager, ILogger<CreateRoleClaimCommandHandler> logger)
        {
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task<Result> Handle(CreateRoleClaimCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(request.RoleId);
            if (null == role)
            {
                var e = new NotFoundException(nameof(role), request.RoleId);
                _logger.LogError(e.Message);
                return Result.Failure("Role not found!");
            }

            await _roleManager.AddClaimAsync(role, new System.Security.Claims.Claim(ClaimTypes.Permissions.GetAttributeStringValue(), request.ClaimValue));

            return Result.Success("Role claim saved!", role);
        }
    }

    public class CreateRoleClaimCommandValidator : AbstractValidator<CreateRoleClaimCommand>
    {
        public CreateRoleClaimCommandValidator()
        {
            RuleFor(x => x.RoleId).NotEmpty().WithMessage("Role Id cannot be empty!");
            RuleFor(x => x.ClaimValue).NotEmpty().WithMessage("Claim value cannot be empty");
        }
    }
}
