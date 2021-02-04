using Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Setup.Roles.Commands.CreateRole
{
    public class CreateRoleCommand : IRequest<Result>
    {
        public string RoleName { get; set; }
    }

    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Result>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<CreateRoleCommandHandler> _logger;

        public CreateRoleCommandHandler(RoleManager<IdentityRole> roleManager, ILogger<CreateRoleCommandHandler> logger)
        {
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task<Result> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            if(!await _roleManager.RoleExistsAsync(request.RoleName))
            {
                IdentityRole role = new IdentityRole();
                role.Name = request.RoleName;
                IdentityResult roleResult = await _roleManager.CreateAsync(role);
                if (roleResult.Succeeded)
                {
                    return Result.Success("Role created!", role);
                }

                return Result.Failure("Role not created!");
            }

            return Result.Failure("Role exist!");
        }
    }

    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            RuleFor(x => x.RoleName).NotEmpty().WithMessage("Role name cannot be empty!");
        }
    }
}
