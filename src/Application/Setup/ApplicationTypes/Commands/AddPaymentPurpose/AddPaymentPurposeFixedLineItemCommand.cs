using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Setup.ApplicationTypes.Commands.AddPaymentPurpose
{
    public class AddPaymentPurposeFixedLineItemCommand : IRequest<Result>
    {
        public Guid PaymentPurposeId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public bool IsTaxable { get; set; }
        public decimal Tax { get; set; }
        public int Weight { get; set; }
        public bool IsSelectable { get; set; }
    }

    public class AddPaymentPurposeFixedLineItemCommandHandler : IRequestHandler<AddPaymentPurposeFixedLineItemCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<AddPaymentPurposeFixedLineItemCommandHandler> _logger;

        public AddPaymentPurposeFixedLineItemCommandHandler(IApplicationDbContext context, ILogger<AddPaymentPurposeFixedLineItemCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Result> Handle(AddPaymentPurposeFixedLineItemCommand request, CancellationToken cancellationToken)
        {
            var entity = new PaymentPurposeFixedLineItem()
            {
                PaymentPurposeId = request.PaymentPurposeId,
                Name = request.Name,
                Amount =  request.Amount,
                IsTaxable = request.IsTaxable,
                IsSelectable = request.IsSelectable,
                Tax = request.Tax,
                Weight = request.Weight
            };

            _context.PaymentPurposeFixedLineItems.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(entity);
        }
    }


    public class AddPaymentPurposeFixedLineItemCommandValidator : AbstractValidator<AddPaymentPurposeFixedLineItemCommand>
    {
        private readonly IApplicationDbContext _context;
        public AddPaymentPurposeFixedLineItemCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.PaymentPurposeId).NotEmpty().WithMessage("Select payment purpose").MustAsync(PaymentPurposeExist).WithMessage("Payment purpose must exist!");
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Amount).NotEmpty();
            RuleFor(x => x.Weight).NotEmpty();
        }

        private async Task<bool> PaymentPurposeExist(Guid paymentPurposeId, CancellationToken cancellationToken)
        {
            var d = await _context.PaymentPurposes.FindAsync(paymentPurposeId);

            return null != d;
        }
    }
}
