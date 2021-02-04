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
    public class UpdatePaymentPurposeFixedLineItemCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public Guid PaymentPurposeId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public bool IsTaxable { get; set; }
        public decimal Tax { get; set; }
        public int Weight { get; set; }
        public bool IsSelectable { get; set; }
    }

    public class UpdatePaymentPurposeFixedLineItemCommandHandler : IRequestHandler<UpdatePaymentPurposeFixedLineItemCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<UpdatePaymentPurposeFixedLineItemCommandHandler> _logger;

        public UpdatePaymentPurposeFixedLineItemCommandHandler(IApplicationDbContext context, ILogger<UpdatePaymentPurposeFixedLineItemCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Result> Handle(UpdatePaymentPurposeFixedLineItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.PaymentPurposeFixedLineItems.FindAsync(request.Id); 
            if (null == entity)
            {
                var e = new NotFoundException(nameof(entity), request.Id);
                _logger.LogError(e.Message);

                return Result.Failure("Payment purpose not found!");
            }

            entity.PaymentPurposeId = request.PaymentPurposeId;
            entity.Name = request.Name;
            entity.Amount = request.Amount;
            entity.IsTaxable = request.IsTaxable;
            entity.Tax = request.Tax;
            entity.Weight = request.Weight;
            entity.IsSelectable = request.IsSelectable;

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success("Payment purpose fixed line item updated!");
        }
    }

    public class UpdatePaymentPurposeFixedLineItemCommandValidator : AbstractValidator<UpdatePaymentPurposeFixedLineItemCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdatePaymentPurposeFixedLineItemCommandValidator(IApplicationDbContext context)
        {
            _context = context;
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.PaymentPurposeId).NotEmpty().WithMessage("Select payment purpose")
                .MustAsync(PaymentPurposeExist).WithMessage("Payment purpose must exist!");
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Amount).NotEmpty();
            RuleFor(x => x.Weight).NotEmpty();
            RuleFor(x => x.Tax).NotEmpty();
        }

        private async Task<bool> PaymentPurposeExist(Guid paymentPurposeId, CancellationToken cancellationToken)
        {
            var d = await _context.PaymentPurposes.FindAsync(paymentPurposeId);

            return null != d;
        }
    }
}
