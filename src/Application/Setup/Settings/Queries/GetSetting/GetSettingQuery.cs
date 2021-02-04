using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Setup.Settings.Queries.GetSetting
{
    public class GetSettingQuery : IRequest<Setting>
    {
        public int Id { get; set; }
    }

    public class GetSettingQueryHandler : IRequestHandler<GetSettingQuery, Setting>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<GetSettingQueryHandler> _logger;

        public GetSettingQueryHandler(IApplicationDbContext context, ILogger<GetSettingQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Setting> Handle(GetSettingQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Settings.FindAsync(request.Id);

            if(null == entity)
            {
                var e = new NotFoundException(nameof(entity), request.Id);
                _logger.LogError(e.Message);
                throw e;
            }

            return entity;
        }
    }
}
