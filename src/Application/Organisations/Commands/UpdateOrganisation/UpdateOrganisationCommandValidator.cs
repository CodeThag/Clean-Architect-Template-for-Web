using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Organisations.Commands.UpdateOrganisation
{
    public class UpdateOrganisationCommandValidator : AbstractValidator<UpdateOrganisationCommand>
    {
        public UpdateOrganisationCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.RCNumber).NotEmpty();
            RuleFor(x => x.TIN).NotEmpty();
            RuleFor(x => x.OrganisationTypeId).NotEmpty();
        }
    }
}
