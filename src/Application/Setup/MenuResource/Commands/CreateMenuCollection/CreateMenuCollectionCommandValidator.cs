using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Setup.MenuResource.Commands.CreateMenuCollection
{
    public class CreateMenuCollectionCommandValidator : AbstractValidator<CreateMenuCollectionCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateMenuCollectionCommandValidator(IApplicationDbContext context)
        {
            _context = context;
            RuleFor(x => x.DisplayName).NotEmpty();
            RuleFor(x => x.SystemName).NotEmpty().WithMessage("System name cannot be empty").MustAsync(Unique).WithMessage("Must be unique!");
        }

        private async Task<bool> Unique(string systemName, CancellationToken cancellationToken)
        {
            var collection = await _context.MenuCollections.FirstOrDefaultAsync(x => x.SystemName == systemName, cancellationToken);
            return null == collection;
        }
    }
}
