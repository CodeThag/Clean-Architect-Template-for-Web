using System;
using System.Collections.Generic;
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

namespace Application.Setup.PaymentTypes.Commands.CreatePaymentType
{
    public class CreatePaymentTypeCommand : IRequest<Result>
    {
        public string Name { get; set; }
    }

    public class CreatePaymentTypeCommandHandler : IRequestHandler<CreatePaymentTypeCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<CreatePaymentTypeCommandHandler> _logger;

        public CreatePaymentTypeCommandHandler(IApplicationDbContext context, ILogger<CreatePaymentTypeCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Result> Handle(CreatePaymentTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = new PaymentType
            {
                Name = request.Name
            };

            _context.PaymentTypes.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success("Payment type created!", entity);
        }
    }

    public class CreatePaymentTypeCommandValidator : AbstractValidator<CreatePaymentTypeCommand>
    {
        public CreatePaymentTypeCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
