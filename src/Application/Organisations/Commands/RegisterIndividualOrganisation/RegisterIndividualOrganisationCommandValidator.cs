using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Organisations.Commands.RegisterIndividualOrganisation
{
    public class RegisterIndividualOrganisationCommandValidator: AbstractValidator<RegisterIndividualOrganisationCommand>
    {
        public RegisterIndividualOrganisationCommandValidator()
        {
            RuleFor(x => x.Surname).NotEmpty();
            RuleFor(x => x.Firstname).NotEmpty();
            RuleFor(x => x.GenderId).NotEmpty();
            RuleFor(x => x.PhoneNumber).NotEmpty();
        }
    }
}
