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

namespace Application.Setup.MenuResource.Commands.CreateMenuCollection
{
   public class CreateMenuCollectionCommand : IRequest<Result>
    {
        public string DisplayName { get; set; }
        public string SystemName { get; set; }
    }
    public class CreateMenuCollectionCommandHandler : IRequestHandler<CreateMenuCollectionCommand, Result>
    {

        private readonly IApplicationDbContext _context;
        private readonly ILogger<CreateMenuCollectionCommandHandler> _logger;

        public CreateMenuCollectionCommandHandler(IApplicationDbContext context, ILogger<CreateMenuCollectionCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Result> Handle(CreateMenuCollectionCommand request, CancellationToken cancellationToken)
        {
            var collection = new MenuCollection
            {
                DisplayName = request.DisplayName,
                SystemName = request.SystemName
            };

            _context.MenuCollections.Add(collection);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success("Menu collection created!", collection);
        }
    }

    
}
