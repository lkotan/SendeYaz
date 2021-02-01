using FluentValidation;
using SendeYaz.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SendeYaz.Business.Validations
{
    public class RuleValidator:AbstractValidator<Rule>
    {
        public RuleValidator()
        {
            RuleFor(x => x.ApplicationModule).IsInEnum();
        }
    }
}
