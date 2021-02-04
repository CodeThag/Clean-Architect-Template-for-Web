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

namespace Application.Setup.ApplicationTypes.Commands.UpdatePaymentPurpose
{
    public class UpdatePaymentPurposeCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public Guid PaymentTypeId { get; set; }
        public Guid ApplicationTypeId { get; set; }
        public string SystemName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class UpdatePaymentPurposeCommandHandler : IRequestHandler<UpdatePaymentPurposeCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<UpdatePaymentPurposeCommandHandler> _logger;

        public UpdatePaymentPurposeCommandHandler(IApplicationDbContext context, ILogger<UpdatePaymentPurposeCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Result> Handle(UpdatePaymentPurposeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.PaymentPurposes.FindAsync(request.Id);
            if (null == entity)
            {
                var e = new NotFoundException(nameof(entity), request.Id);
                _logger.LogError(e.Message);
                return Result.Failure("Payment purpose not found!");
            }

            entity.PaymentTypeId = request.PaymentTypeId;
            entity.ApplicationTypeId = request.ApplicationTypeId;
            entity.SystemName = request.SystemName;
            entity.Name = request.Name;
            entity.Description = request.Description;

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success("Payment purpose updated!",entity);
        }
    }

    public class UpdatePaymentPurposeCommandValidator : AbstractValidator<UpdatePaymentPurposeCommand>
    {
        public UpdatePaymentPurposeCommandValidator()
        {
            RuleFor(x => x.PaymentTypeId).NotEmpty();
            RuleFor(x => x.ApplicationTypeId).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
