using FluentValidation;
using SendeYaz.Entities;
using SendeYaz.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SendeYaz.Business.Validations
{
    public class RoleValidator:AbstractValidator<RoleModel>
    {
        public RoleValidator()
        {
            RuleFor(x => x.Description).Length(3, 100);
        }
    }
}
