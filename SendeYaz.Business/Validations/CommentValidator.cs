using FluentValidation;
using SendeYaz.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SendeYaz.Business.Validations
{
    public class CommentValidator:AbstractValidator<CommentModel>
    {
        public CommentValidator()
        {
            RuleFor(x => x.BlogId).GreaterThan(0);
            RuleFor(x => x.Description).NotNull().NotEmpty();
        }
    }
}
