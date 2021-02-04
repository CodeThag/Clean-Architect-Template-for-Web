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

namespace Application.Setup.Departments.Commands.DeleteDepartment
{
    public class DeleteDepartmentCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }

    public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<DeleteDepartmentCommandHandler> _logger;

        public DeleteDepartmentCommandHandler(IApplicationDbContext context, ILogger<DeleteDepartmentCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Result> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Departments.FindAsync(request.Id);
            if (null == entity)
            {
                var e = new NotFoundException(nameof(entity), request.Id);
                _logger.LogError(e.Message);
                return Result.Failure(e.Message);
            }

            _context.Departments.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success("Department deleted!");
        }
    }

    public class DeleteDepartmentCommandValidator : AbstractValidator<DeleteDepartmentCommand>
    {
        public DeleteDepartmentCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
