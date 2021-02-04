using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;

namespace Application.Setup.ApplicationTypes.Queries.GetPaymentPurpose
{
    public class GetPaymentPurposeQuery : IRequest<Result>
    {
        public Guid Id { get; set; }
    }

    public class GetPaymentPurposeQueryHandler : IRequestHandler<GetPaymentPurposeQuery, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<GetPaymentPurposeQueryHandler> _logger;

        public GetPaymentPurposeQueryHandler(IApplicationDbContext context, ILogger<GetPaymentPurposeQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Result> Handle(GetPaymentPurposeQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.PaymentPurposes.FindAsync(request.Id);
            if (null == entity)
            {
                var e = new NotFoundException(nameof(entity));
                _logger.LogError(e.Message);
                return Result.Failure("Payment purpose not found!");
            }

            return Result.Success(entity);
        }
    }
}
