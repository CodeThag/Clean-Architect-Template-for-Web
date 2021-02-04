using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Setup.Departments.Commands.CreateDepartment
{
   public class CreateDepartmentCommand : IRequest<Result>
    {
        public string Name { get; set; }
        public string Acronym { get; set; }
        public string Description { get; set; }
        public bool HasApplicationProcess { get; set; }
    }

   public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, Result>
   {
       private readonly IApplicationDbContext _context;
       private readonly ILogger<CreateDepartmentCommandHandler> _logger;

       public CreateDepartmentCommandHandler(IApplicationDbContext context, ILogger<CreateDepartmentCommandHandler> logger)
       {
           _context = context;
           _logger = logger;
       }

       public async Task<Result> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
       {
           var entity = new Department
           {
               Name = request.Name,
               Acronym = request.Acronym,
               Description = request.Description,
               HasApplicationProcess = request.HasApplicationProcess
           };

           _context.Departments.Add(entity);

           await _context.SaveChangesAsync(cancellationToken);

           return Result.Success("Department saved!", entity);
       }
   }

   public class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
    {
        public CreateDepartmentCommandValidator()
        {
            RuleFor(x => x.Acronym).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}