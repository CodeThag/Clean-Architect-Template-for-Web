using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Setup.ApplicationTypes.Commands.UpdateApplicationType
{
    public class UpdateApplicationTypeCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool HasWorkflow { get; set; }
        public string WorkflowCode { get; set; }
        public Guid DepartmentId { get; set; }
        public string Guidelines { get; set; }
    }

    public class UpdateApplicationTypeCommandHandler : IRequestHandler<UpdateApplicationTypeCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<UpdateApplicationTypeCommandHandler> _logger;

        public UpdateApplicationTypeCommandHandler(IApplicationDbContext context, ILogger<UpdateApplicationTypeCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Result> Handle(UpdateApplicationTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.ApplicationTypes.FindAsync(request.Id);
            if (null == entity)
            {
                var e = new NotFoundException(nameof(entity), request.Id);
                _logger.LogError(e.Message);
                return Result.Failure(e.Message);
            }

            entity.DepartmentId = request.DepartmentId;
            entity.Description = request.Description;
            entity.Guidelines = request.Guidelines;
            entity.HasWorkflow = request.HasWorkflow;
            entity.IsActive = request.IsActive;
            entity.Name = request.Name;
            entity.WorkflowCode = request.WorkflowCode;

            if (request.HasWorkflow)
            {
                if (string.IsNullOrEmpty(request.WorkflowCode))
                {
                    var message = "Workflow code cannot be null";
                    _logger.LogError(message);
                    return Result.Failure(message);
                }

                var scheme = await _context.WorkflowSchemes.FirstOrDefaultAsync(x => x.Code == request.WorkflowCode, cancellationToken);
                if (null == scheme)
                {
                    scheme = new WorkflowScheme() { Scheme = "", Code = request.WorkflowCode };
                    _context.WorkflowSchemes.Add(scheme);
                }
                else
                {
                    scheme.Code = request.WorkflowCode;
                }
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(entity);
        }
    }


    public class UpdateApplicationTypeCommandValidator : AbstractValidator<UpdateApplicationTypeCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateApplicationTypeCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.DepartmentId).NotEmpty().WithMessage("Department cannot be empty!").MustAsync(DepartmentExist).WithMessage("Department must exist");
            RuleFor(x => x.WorkflowCode).NotEmpty();
        }

        private async Task<bool> DepartmentExist(Guid departmentId, CancellationToken cancellationToken)
        {
            var d = await _context.Departments.FindAsync(departmentId);

            return null != d;
        }
    }
}
