using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Setup.MenuResource.Commands.UpdateMenuCollection
{
    public class UpdateMenuCollectionCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string SystemName { get; set; }
    }

    public class UpdateMenuCollectionCommandHandler : IRequestHandler<UpdateMenuCollectionCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<UpdateMenuCollectionCommandHandler> _logger;

        public UpdateMenuCollectionCommandHandler(IApplicationDbContext context, ILogger<UpdateMenuCollectionCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Result> Handle(UpdateMenuCollectionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.MenuCollections.FindAsync(request.Id);
            if(null == entity)
            {
                var e = new NotFoundException(nameof(entity), request.Id);
                _logger.LogError(e.Message);
                return Result.Failure(e.Message);
            }

            entity.DisplayName = request.DisplayName;
            entity.SystemName = request.SystemName;

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success("Menu collection updated!");
        }
    }
    public class UpdateMenuCollectionCommandValidator : AbstractValidator<UpdateMenuCollectionCommand>
    {
        public UpdateMenuCollectionCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.SystemName).NotEmpty().WithMessage("System name cannot be empty!");
            RuleFor(x => x.DisplayName).NotEmpty().WithMessage("Display name cannot be empty!");
        }
    }
}
