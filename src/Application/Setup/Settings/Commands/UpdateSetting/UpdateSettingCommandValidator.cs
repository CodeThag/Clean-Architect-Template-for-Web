using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Setup.Settings.Commands.UpdateSetting
{
    public class UpdateSettingCommandValidator : AbstractValidator<UpdateSettingCommand>
    {
        public UpdateSettingCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Type).NotEmpty();
            RuleFor(x => x.Value).NotEmpty();
        }
    }
}
