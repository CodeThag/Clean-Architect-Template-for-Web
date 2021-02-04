using Application.Common.Exceptions;
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

namespace Application.Organisations.Commands.UpdateOrganisation
{
    public class UpdateOrganisationCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string TIN { get; set; }
        public string RCNumber { get; set; }
        public string BVN { get; set; }
        public Guid OrganisationTypeId { get; set; }
        public bool IsActive { get; set; }
        public bool IsUnderReview { get; set; }
    }

    public class UpdateOrganisationCommandHandler : IRequestHandler<UpdateOrganisationCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<UpdateOrganisationCommandHandler> _logger;

        public UpdateOrganisationCommandHandler(IApplicationDbContext context, ILogger<UpdateOrganisationCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Result> Handle(UpdateOrganisationCommand request, CancellationToken cancellationToken)
        {
            Organisation entity = await _context.Organisations.FindAsync(request.Id);
            if (null == entity)
            {
                var e = new NotFoundException(nameof(entity), request.Id);
                _logger.LogError(e.Message);
                return Result.Failure(e.Message);
            }

            entity.Name = request.Name;
            entity.OrganisationTypeId = request.OrganisationTypeId;
            entity.BVN = entity.BVN;
            entity.TIN = entity.TIN;
            entity.RCNumber = entity.RCNumber;
            entity.IsActive = request.IsActive;
            entity.IsUnderReview = request.IsUnderReview;

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success("Organisation details updated!", entity);
        }
    }
}
