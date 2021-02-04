using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Setup.Settings.Commands.UpdateSetting
{
    public class UpdateSettingCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }

    public class UpdateSettingCommandHandler : IRequestHandler<UpdateSettingCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<UpdateSettingCommandHandler> _logger;

        public UpdateSettingCommandHandler(IApplicationDbContext context, ILogger<UpdateSettingCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Result> Handle(UpdateSettingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _context.Settings.FindAsync(request.Id);
                if (null == entity)
                {
                    var e = new NotFoundException(nameof(entity), request.Id);
                    _logger.LogError(e.Message);
                    return Result.Failure("Record not found!");
                }

                entity.Name = request.Name;
                entity.Type = request.Type;
                entity.Value = request.Value;

                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success("Settings updated!");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return Result.Failure("Error occurred: Settings not updated!");
            }
        }
    }
}
