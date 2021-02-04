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

namespace Application.Organisations.Commands.CreateOrganisation
{
    public class CreateOrganisationCommand : IRequest<Result>
    {
        public string Name { get; set; }
        public string TIN { get; set; }
        public string RCNumber { get; set; }
        public string BVN { get; set; }
        public Guid OrganisationTypeId { get; set; }
    }

    public class CreateOrganisationCommandHandler : IRequestHandler<CreateOrganisationCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<CreateOrganisationCommandHandler> _logger;

        public CreateOrganisationCommandHandler(IApplicationDbContext context, ILogger<CreateOrganisationCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Result> Handle(CreateOrganisationCommand request, CancellationToken cancellationToken)
        {
            var entity = new Organisation
            {
                Name = request.Name,
                TIN = request.TIN,
                RCNumber = request.RCNumber,
                OrganisationTypeId = request.OrganisationTypeId,
                IsActive = true,
                IsUnderReview = true
            };

            _context.Organisations.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success("Organisation details saved!");
        }
    }
}
