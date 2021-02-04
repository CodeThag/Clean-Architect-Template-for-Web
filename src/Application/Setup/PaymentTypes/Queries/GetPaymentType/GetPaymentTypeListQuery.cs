using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Setup.PaymentTypes.Queries.GetPaymentType
{
    public class GetPaymentTypeListQuery : IRequest<Result>
    {
    }

    public class GetPaymentTypeListQueryHandler : IRequestHandler<GetPaymentTypeListQuery, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<GetPaymentTypeListQueryHandler> _logger;

        public GetPaymentTypeListQueryHandler(IApplicationDbContext context, ILogger<GetPaymentTypeListQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Result> Handle(GetPaymentTypeListQuery request, CancellationToken cancellationToken)
        {
            var list = await _context.PaymentTypes.ToListAsync();
            return Result.Success(list);
        }
    }
}
