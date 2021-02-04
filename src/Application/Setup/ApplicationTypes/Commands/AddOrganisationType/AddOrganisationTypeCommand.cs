using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Setup.ApplicationTypes.Commands.AddOrganisationType
{
    public class AddOrganisationTypeCommand : IRequest<Result>
    {
        public Guid ApplicationTypeId { get; set; }
        public Guid OrganisationTypeId { get; set; }
    }

    public class AddOrganisationTypeCommandHandler : IRequestHandler<AddOrganisationTypeCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<AddOrganisationTypeCommandHandler> _logger;

        public AddOrganisationTypeCommandHandler(IApplicationDbContext context, ILogger<AddOrganisationTypeCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Result> Handle(AddOrganisationTypeCommand request, CancellationToken cancellationToken)
        {
            var applicationType = await _context.ApplicationTypes.FindAsync(request.ApplicationTypeId);
            if (null == applicationType)
            {
                var e = new NotFoundException(nameof(applicationType));
                _logger.LogError(e.Message);
                return Result.Failure("Application type not found!");
            }

            var organisationType = await _context.OrganisationTypes.FindAsync(request.OrganisationTypeId);
            if (null == organisationType)
            {
                var e = new NotFoundException(nameof(organisationType), request.OrganisationTypeId);
                _logger.LogError(e.Message);
                return Result.Failure("Organisation type not found!");
            }

            applicationType.OrganisationTypes.Add(organisationType);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(applicationType);
        }
    }

    public class AddOrganisationTypeCommandValidator : AbstractValidator<AddOrganisationTypeCommand>
    {
        public AddOrganisationTypeCommandValidator()
        {
            RuleFor(x => x.ApplicationTypeId).NotEmpty();
            RuleFor(x => x.OrganisationTypeId).NotEmpty();
        }
    }
}
