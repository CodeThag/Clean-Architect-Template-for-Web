using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Setup.ApplicationTypes.Commands.CreateApplicationType
{
    public class CreateApplicationTypeCommand : IRequest<Result>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool HasWorkflow { get; set; }
        public string WorkflowCode { get; set; }
        public Guid DepartmentId { get; set; }
        public string Guidelines { get; set; }
    }

    public class CreateApplicationTypeCommandHandler : IRequestHandler<CreateApplicationTypeCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<CreateApplicationTypeCommandHandler> _logger;

        public CreateApplicationTypeCommandHandler(IApplicationDbContext context, ILogger<CreateApplicationTypeCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Result> Handle(CreateApplicationTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = new ApplicationType()
            {
                Name = request.Name,
                Description = request.Description,
                IsActive = request.IsActive,
                HasWorkflow = request.HasWorkflow,
                WorkflowCode = request.WorkflowCode,
                DepartmentId = request.DepartmentId,
                Guidelines = request.Guidelines
            };

            await _context.ApplicationTypes.AddAsync(entity, cancellationToken);

            if (request.HasWorkflow)
            {
                var scheme = new WorkflowScheme()
                {
                    Code = request.WorkflowCode,
                    Scheme = ""
                };
                await _context.WorkflowSchemes.AddAsync(scheme, cancellationToken);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(entity);
        }
    }

    public class CreateApplicationTypeCommandValidator : AbstractValidator<CreateApplicationTypeCommand>
    {
        private readonly IApplicationDbContext _context;
        public CreateApplicationTypeCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Guidelines).NotEmpty();
            RuleFor(x => x.DepartmentId).NotEmpty().MustAsync(DepartmentExist).WithMessage("Department must exist");
            RuleFor(x => x.WorkflowCode).NotEmpty();
        }

        private async Task<bool> DepartmentExist(Guid departmentId, CancellationToken cancellationToken)
        {
            var d = await _context.Departments.FindAsync(departmentId);

            return null != d;
        }
    }
}