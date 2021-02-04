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
using Microsoft.Extensions.Logging;

namespace Application.Setup.PaymentTypes.Queries.GetPaymentType
{
    public class GetPaymentTypeQuery : IRequest<Result>
    {
        public Guid Id { get; set; }
    }

    public class GetPaymentTypeQueryHandler : IRequestHandler<GetPaymentTypeQuery, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<GetPaymentTypeQueryHandler> _logger;

        public GetPaymentTypeQueryHandler(IApplicationDbContext context, ILogger<GetPaymentTypeQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Result> Handle(GetPaymentTypeQuery request, CancellationToken cancellationToken)
        {
            var entity =  _context.PaymentTypes.Find(request.Id);
            if (null == entity)
            {
                var e = new NotFoundException(nameof(entity));
                _logger.LogError(e.Message);
                return Result.Failure("Payment type not found");
            }

            return Result.Success(entity);
        }
    }
}
