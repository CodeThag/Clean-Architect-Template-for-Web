using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Setup.MenuResource.Commands.DeleteMenuItem;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Setup.MenuResource.Commands.DeleteMenuCollection
{
    public class DeleteMenuCollectionCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }

    public class DeleteMenuCollectionCommandHandler : IRequestHandler<DeleteMenuCollectionCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<DeleteMenuCollectionCommandHandler> _logger;

        public DeleteMenuCollectionCommandHandler(IApplicationDbContext context, ILogger<DeleteMenuCollectionCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Result> Handle(DeleteMenuCollectionCommand request, CancellationToken cancellationToken)
        {
            var collection = await _context.MenuCollections.FindAsync(request.Id);
            if (null == collection)
            {
                var e = new NotFoundException(nameof(collection), request.Id);
                _logger.LogError(e.Message);
                return Result.Failure(e.Message);
            }

            var items = collection.MenuItems.ToList();

            _context.MenuItems.RemoveRange(items);

            _context.MenuCollections.Remove(collection);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success("Menu collection deleted!");
        }
    }

    public class DeleteMenuCollectionCommandValidator: AbstractValidator<DeleteMenuCollectionCommand>
    {
        public DeleteMenuCollectionCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
