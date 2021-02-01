using FluentValidation;
using SendeYaz.Entities;
using SendeYaz.Models;

namespace SendeYaz.Business.Validations
{
    public class CategoryValidator:AbstractValidator<CategoryModel>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Kategori Adı boş olamaz.");

            RuleFor(x => x.Name).Length(3,100).WithMessage("Kategori Adı en az 3 en fazla 100 karakter olmalıdır.");
        }
    }
}
