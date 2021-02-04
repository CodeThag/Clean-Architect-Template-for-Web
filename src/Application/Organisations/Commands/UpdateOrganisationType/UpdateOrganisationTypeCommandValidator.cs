using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Organisations.Commands.UpdateOrganisationType
{
    public class UpdateOrganisationTypeCommandValidator : AbstractValidator<UpdateOrganisationTypeCommand>
    {
        public UpdateOrganisationTypeCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
