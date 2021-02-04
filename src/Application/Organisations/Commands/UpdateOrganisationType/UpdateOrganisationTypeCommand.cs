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

namespace Application.Organisations.Commands.UpdateOrganisationType
{
    public class UpdateOrganisationTypeCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
    }

    public class UpdateOrganisationTypeCommandHandler : IRequestHandler<UpdateOrganisationTypeCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<UpdateOrganisationTypeCommandHandler> _logger;

        public UpdateOrganisationTypeCommandHandler(IApplicationDbContext context, ILogger<UpdateOrganisationTypeCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Result> Handle(UpdateOrganisationTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.OrganisationTypes.FindAsync(request.Id);
            if (null == entity)
            {
                var e = new NotFoundException(nameof(entity), request.Id);
                _logger.LogError(e.Message);
                return Result.Failure(e.Message);
            }

            entity.Name = request.Name;
            entity.Icon = request.Icon;

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success("Organisation type updated!");
        }
    }
}
