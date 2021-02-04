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

namespace Application.Organisations.Commands.CreateOrganisationType
{
    public class CreateOrganisationTypeCommand : IRequest<Result>
    {
        public string Name { get; set; }
        public string Icon { get; set; }
    }

    public class CreateOrganisationTypeCommandHandler : IRequestHandler<CreateOrganisationTypeCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<CreateOrganisationTypeCommandHandler> _logger;

        public CreateOrganisationTypeCommandHandler(IApplicationDbContext context, ILogger<CreateOrganisationTypeCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Result> Handle(CreateOrganisationTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = new OrganisationType
            {
                Name = request.Name,
                Icon = request.Icon
            };

            _context.OrganisationTypes.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success("Organisation type created!");
        }
    }
}
