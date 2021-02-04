using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UserProfileConfiguation.Commands.UpdateOrganisationProfile
{
    public class UpdateOrganisationProfileCommandValidator : AbstractValidator<UpdateOrganisationProfileCommand>
    {
        public UpdateOrganisationProfileCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
