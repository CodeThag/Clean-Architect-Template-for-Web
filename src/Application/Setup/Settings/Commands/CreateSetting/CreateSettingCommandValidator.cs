using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Setup.Settings.Commands.CreateSetting
{
    public class CreateSettingCommandValidator : AbstractValidator<CreateSettingCommand>
    {
        public CreateSettingCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Type).NotEmpty();
            RuleFor(x => x.Value).NotEmpty();
        }
    }
}
