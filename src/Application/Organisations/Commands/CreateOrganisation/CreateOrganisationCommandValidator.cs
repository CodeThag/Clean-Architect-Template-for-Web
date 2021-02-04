using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Organisations.Commands.CreateOrganisation
{
    public class CreateOrganisationCommandValidator : AbstractValidator<CreateOrganisationCommand>
    {
        public CreateOrganisationCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.TIN).NotEmpty();
            RuleFor(x => x.OrganisationTypeId).NotEmpty();
            RuleFor(x => x.RCNumber).NotEmpty();
            RuleFor(x => x.BVN).NotEmpty();
        }
    }
}
