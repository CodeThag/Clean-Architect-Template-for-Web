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

namespace Application.Setup.MenuResource.Commands.UpdateMenuItem
{
    public class UpdateMenuItemCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Area { get; set; }
        public string PageLink { get; set; }
        public bool IsCollapsible { get; set; }
        public string Label { get; set; }
        public int MenuCollectionId { get; set; }
        public int Weight { get; set; }
        public string Permission { get; set; }
        public string Icon { get; set; }
    }

    public class UpdateMenuItemCommandHandler : IRequestHandler<UpdateMenuItemCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<UpdateMenuItemCommandHandler> _logger;

        public UpdateMenuItemCommandHandler(IApplicationDbContext context, ILogger<UpdateMenuItemCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Result> Handle(UpdateMenuItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.MenuItems.FindAsync(request.Id);
            if(null == entity)
            {
                var e = new NotFoundException(nameof(entity), request.Id);
                _logger.LogError(e.Message);
                return Result.Failure("Menu item not found!");
            }

            entity.Area = request.Area;
            entity.Label = request.Label;
            entity.IsCollapsible = request.IsCollapsible;
            entity.PageLink = request.PageLink;
            entity.ParentId = request.ParentId;
            entity.Weight = request.Weight;
            entity.MenuCollectionId = request.MenuCollectionId;
            entity.Permission = request.Permission;
            entity.Icon = request.Icon;

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success("Menu item updated!", entity);
        }
    }

    public class UpdateMenuItemCommandValidator : AbstractValidator<UpdateMenuItemCommand>
    {
        public UpdateMenuItemCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Label).NotEmpty().WithMessage("Label cannot be empty!");
            RuleFor(x => x.IsCollapsible).NotEmpty().WithMessage("Is Collapsible must be selected!");
            RuleFor(x => x.Weight).NotEmpty().WithMessage("Weight cannot be empty!");
        }
    }
}