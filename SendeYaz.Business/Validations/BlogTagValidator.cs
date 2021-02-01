using FluentValidation;
using SendeYaz.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SendeYaz.Business.Validations
{
    public class BlogTagValidator:AbstractValidator<BlogTag>
    {
        public BlogTagValidator()
        {
            RuleFor(x => x.BlogId).GreaterThan(1);
            RuleFor(x => x.TagId).GreaterThan(1);
        }
    }
}
