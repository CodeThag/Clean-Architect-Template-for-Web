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
using Microsoft.Extensions.Logging;

namespace Application.Setup.ApplicationTypes.Commands.AddPaymentPurpose
{
    public class AddPaymentPurposeCommand : IRequest<Result>
    {
        public Guid PaymentTypeId { get; set; }
        public Guid ApplicationTypeId { get; set; }
        public string SystemName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class AddPaymentPurposeCommandHandler : IRequestHandler<AddPaymentPurposeCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<AddPaymentPurposeCommandHandler> _logger;

        public AddPaymentPurposeCommandHandler(IApplicationDbContext context, ILogger<AddPaymentPurposeCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Result> Handle(AddPaymentPurposeCommand request, CancellationToken cancellationToken)
        {
            var applicationType = await _context.ApplicationTypes.FindAsync(request.ApplicationTypeId);
            if (null == applicationType)
            {
                var e = new NotFoundException(nameof(applicationType), request.ApplicationTypeId);
                _logger.LogError(e.Message);
                return Result.Failure("Application type not found!");
            }

            var paymentType = await _context.PaymentTypes.FindAsync(request.PaymentTypeId);
            if (null == paymentType)
            {
                var e = new NotFoundException(nameof(paymentType));
                _logger.LogError(e.Message);
                return Result.Failure("Payment type not found!");
            }

            var entity = new PaymentPurpose()
            {
                SystemName = request.SystemName,
                Name = request.Name,
                Description = request.Description,
                ApplicationType = applicationType,
                PaymentType =  paymentType
            };

            _context.PaymentPurposes.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success("Payment purpose created!", entity);
        }
    }

    public class AddPaymentPurposeCommandValidator : AbstractValidator<AddPaymentPurposeCommand>
    {
        public AddPaymentPurposeCommandValidator()
        {
            RuleFor(x => x.PaymentTypeId).NotEmpty();
            RuleFor(x => x.ApplicationTypeId).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
