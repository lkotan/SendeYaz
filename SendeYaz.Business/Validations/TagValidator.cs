using FluentValidation;
using SendeYaz.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SendeYaz.Business.Validations
{
    public class TagValidator:AbstractValidator<Tag>
    {
        public TagValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Name).Length(3, 100);
        }
    }
}
