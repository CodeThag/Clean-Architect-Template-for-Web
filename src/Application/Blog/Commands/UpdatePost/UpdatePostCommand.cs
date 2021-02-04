using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Models;
using FluentValidation;
using MediatR;

namespace Application.Blog.Commands.UpdatePost
{
    public class UpdatePostCommand : IRequest<Result>
    {
    }

    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, Result>
    {
        public Task<Result> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    public class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
    {
        public UpdatePostCommandValidator()
        {

        }
    }
}
