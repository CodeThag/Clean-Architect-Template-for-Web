using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Setup.MenuResource.Commands.DeleteMenuItem
{
    public class DeleteMenuItemCommand :IRequest<Result>
    {
        public int MenuCollectionId { get; set; }
        public int ItemId { get; set; }
    }

    public class DeleteMenuItemCommandHandler : IRequestHandler<DeleteMenuItemCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<DeleteMenuItemCommandHandler> _logger;

        public DeleteMenuItemCommandHandler(IApplicationDbContext context, ILogger<DeleteMenuItemCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Result> Handle(DeleteMenuItemCommand request, CancellationToken cancellationToken)
        {
            var collection = await _context.MenuCollections.FindAsync(request.MenuCollectionId);
            if(null == collection)
            {
                var e = new NotFoundException(nameof(collection), request.MenuCollectionId);
                _logger.LogError(e.Message);
                return Result.Failure(e.Message);
            }

            var item = collection.MenuItems.FirstOrDefault(x => x.Id == request.ItemId);
            _context.MenuItems.Remove(item);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success("Menu item deleted!");
        }
    }


}
