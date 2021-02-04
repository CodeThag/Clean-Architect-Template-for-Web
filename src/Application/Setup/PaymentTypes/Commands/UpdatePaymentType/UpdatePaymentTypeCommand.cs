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

namespace Application.Setup.PaymentTypes.Commands.UpdatePaymentType
{
    public class UpdatePaymentTypeCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class UpdatePaymentTypeCommandHandler : IRequestHandler<UpdatePaymentTypeCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<UpdatePaymentTypeCommandHandler> _logger;

        public UpdatePaymentTypeCommandHandler(IApplicationDbContext context, ILogger<UpdatePaymentTypeCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Result> Handle(UpdatePaymentTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.PaymentTypes.FindAsync(request.Id);
            if (null == entity)
            {
                var e = new NotFoundException(nameof(entity));
                _logger.LogError(e.Message);
                return Result.Failure("Payment type not found");
            }

            entity.Name = request.Name;

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success("Payment type updated!", entity);
        }
    }

    public class UpdatePaymentTypeCommandValidator : AbstractValidator<UpdatePaymentTypeCommand>
    {
        public UpdatePaymentTypeCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
