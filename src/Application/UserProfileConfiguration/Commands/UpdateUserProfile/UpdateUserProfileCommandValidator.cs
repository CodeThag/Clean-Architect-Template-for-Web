using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UserProfileConfiguation.Commands.UpdateUserProfile
{
    public class UpdateUserProfileCommandValidator : AbstractValidator<UpdateUserProfileCommand>
    {
        public UpdateUserProfileCommandValidator() {
            RuleFor(x => x.Surname).NotEmpty();
            RuleFor(x => x.Firstname).NotEmpty();
            RuleFor(x => x.PhoneNumber).NotEmpty();
            RuleFor(x => x.GenderId).NotEmpty();
        }
    }
}
