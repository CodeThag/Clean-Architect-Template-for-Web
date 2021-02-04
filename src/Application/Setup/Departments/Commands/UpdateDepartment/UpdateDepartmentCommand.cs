using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Setup.Departments.Commands.UpdateDepartment
{
    public class UpdateDepartmentCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Acronym { get; set; }
        public string Description { get; set; }
        public bool HasApplicationProcess { get; set; }
    }

    public  class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<UpdateDepartmentCommandHandler> _logger;

        public UpdateDepartmentCommandHandler(IApplicationDbContext context, ILogger<UpdateDepartmentCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Result> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Departments.FindAsync(request.Id);
            if(null == entity)
            {
                var e = new NotFoundException(nameof(entity), request.Id);
                _logger.LogError(e.Message);
                return Result.Failure(e.Message);
            }

            entity.Acronym = request.Acronym;
            entity.Description = request.Description;
            entity.HasApplicationProcess = request.HasApplicationProcess;
            entity.Name = request.Name;

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success("Department updated!");
        }
    }

    public class UpdateDepartmentCommandValidator : AbstractValidator<UpdateDepartmentCommand>
    {
        public UpdateDepartmentCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Acronym).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.HasApplicationProcess).NotEmpty().WithMessage("Has Application Process cannot be empty!");
        }
    }
}