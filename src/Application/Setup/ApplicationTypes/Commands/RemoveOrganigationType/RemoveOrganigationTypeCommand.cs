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

namespace Application.Setup.ApplicationTypes.Commands.RemoveOrganigationType
{
    public class RemoveOrganigationTypeCommand : IRequest<Result>
    {
        public Guid ApplicationTypeId { get; set; }
        public Guid OrganisationTypeId { get; set; }
    }

    public class RemoveOrganigationTypeCommandHandler : IRequestHandler<RemoveOrganigationTypeCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<RemoveOrganigationTypeCommandHandler> _logger;

        public RemoveOrganigationTypeCommandHandler(IApplicationDbContext context, ILogger<RemoveOrganigationTypeCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Result> Handle(RemoveOrganigationTypeCommand request, CancellationToken cancellationToken)
        {
            var applicationType = await _context.ApplicationTypes.FindAsync(request.ApplicationTypeId);
            if (null == applicationType)
            {
                var e = new NotFoundException(nameof(applicationType));
                _logger.LogError(e.Message);
                return Result.Failure("Application type not found!");
            }

            var organisationType = applicationType.OrganisationTypes.FirstOrDefault(x => x.Id == request.OrganisationTypeId);
            if (null == organisationType)
            {
                var e = new NotFoundException(nameof(organisationType), request.OrganisationTypeId);
                _logger.LogError(e.Message);
                return Result.Failure("Organisation type not found!");
            }

            applicationType.OrganisationTypes.Remove(organisationType);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success("Entity type removed!", applicationType);
        }
    }
}
