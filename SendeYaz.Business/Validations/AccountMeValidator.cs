using FluentValidation;
using SendeYaz.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SendeYaz.Business.Validations
{
    public class AccountMeValidator:AbstractValidator<AccountMeModel>
    {
        public AccountMeValidator()
        {
            RuleFor(x => x.FirstName).Length(3, 25);
            RuleFor(x => x.LastName).Length(3, 25);
            RuleFor(x => x.Email).Length(7, 75).EmailAddress();
        }
    }
}
