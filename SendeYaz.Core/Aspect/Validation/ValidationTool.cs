using FluentValidation;
using System.Linq;
using ValidationException = SendeYaz.Core.Exceptions.ValidationException;

namespace SendeYaz.Core.Aspect.Validation
{
    public static class ValidationTool
    {
        public static void Validate(IValidator validator,object entity)
        {
            var result = validator.Validate(entity);
            if (result.IsValid) return;
            var errors = result.Errors.Aggregate("", (current, error) => $"{current}{error.ErrorMessage}\n");
            throw new ValidationException(errors);
        }
    }
}
