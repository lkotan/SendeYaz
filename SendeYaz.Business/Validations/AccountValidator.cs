using FluentValidation;
using SendeYaz.Entities;
using SendeYaz.Models;

namespace SendeYaz.Business.Validations
{
    public class AccountValidator:AbstractValidator<AccountModel>
    {
        public AccountValidator()
        {
            RuleFor(x => x.AccountType).IsInEnum();
            RuleFor(x => x.FirstName).Length(3, 25);
            RuleFor(x => x.LastName).Length(3, 25);
            RuleFor(x => x.Password).Length(3, 10);
            RuleFor(x => x.Email).Length(7,75).EmailAddress();
        }
    }
}
