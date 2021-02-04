using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Organisations.Commands.CreateOrganisationType
{
    public class CreateOrganisationTypeCommandValidator:AbstractValidator<CreateOrganisationTypeCommand>
    {
        public CreateOrganisationTypeCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
