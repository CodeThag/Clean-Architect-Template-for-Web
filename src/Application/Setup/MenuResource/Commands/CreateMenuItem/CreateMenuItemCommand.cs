using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Setup.MenuResource.Commands.CreateMenuItem
{
    public class CreateMenuItemCommand : IRequest<Result>
    {
        public int? ParentId { get; set; }
        public string Area { get; set; }
        public string PageLink { get; set; }
        public bool IsCollapsible { get; set; }
        public string Label { get; set; }
        public int MenuCollectionId { get; set; }
        public int Weight { get; set; }
        public string Permission { get; set; }
        public string Icon { get; set; }
    }

    public class CreateMenuItemCommandHandler : IRequestHandler<CreateMenuItemCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<CreateMenuItemCommandHandler> _logger;

        public CreateMenuItemCommandHandler(IApplicationDbContext context, ILogger<CreateMenuItemCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Result> Handle(CreateMenuItemCommand request, CancellationToken cancellationToken)
        {
            var collection = await _context.MenuCollections.FindAsync(request.MenuCollectionId);
            if(null == collection)
            {
                var e = new NotFoundException(nameof(collection), request.MenuCollectionId);
                _logger.LogError(e.Message);
                return Result.Failure("Menu collection not found!");
            }

            var entity = new MenuItem
            {
                MenuCollection = collection,
                ParentId = request.ParentId,
                IsCollapsible = request.IsCollapsible,
                PageLink = request.PageLink,
                Area = request.Area,
                Label = request.Label,
                Weight = request.Weight,
                Permission = request.Permission,
                Icon = request.Icon
            };

            _context.MenuItems.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success("Menu item created!", entity);
        }
    }

    public class CreateMenuItemCommandValidator : AbstractValidator<CreateMenuItemCommand>
    {
        public CreateMenuItemCommandValidator()
        {
            RuleFor(x => x.Label).NotEmpty().WithMessage("Label cannot be empty!");
            RuleFor(x => x.Weight).NotEmpty().WithMessage("Weight cannot be empty!");
        }
    }
}
