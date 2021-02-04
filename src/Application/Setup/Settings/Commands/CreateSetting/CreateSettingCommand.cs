using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Setup.Settings.Commands.CreateSetting
{
    public class CreateSettingCommand : IRequest<Result>
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }

    public class CreateSettingCommandHandler : IRequestHandler<CreateSettingCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<CreateSettingCommandHandler> _logger;

        public CreateSettingCommandHandler(IApplicationDbContext context, ILogger<CreateSettingCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Result> Handle(CreateSettingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = new Setting
                {
                    Name = request.Name,
                    Type = request.Type,
                    Value = request.Value
                };

                _context.Settings.Add(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success("Settings saved!", entity);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return Result.Failure("Error occurred: Settings not saved!");
            }
        }
    }
}
