using FluentValidation;
using SendeYaz.Entities;
using SendeYaz.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SendeYaz.Business.Validations
{
    public class BlogValidator:AbstractValidator<BlogModel>
    {
        public BlogValidator()
        {
            RuleFor(x => x.CategoryId).GreaterThan(0);

            RuleFor(x => x.Title).NotNull().NotEmpty();
            RuleFor(x => x.Description).NotNull().NotEmpty();
            RuleFor(x => x.Content).NotNull().NotEmpty();

            RuleFor(x => x.Title).Length(5,100);
            RuleFor(x => x.Description).Length(5,100);

        }
    }
}
