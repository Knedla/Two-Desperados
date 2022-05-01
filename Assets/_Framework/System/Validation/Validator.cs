using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.System.Validation
{
    public class Validator<T> : IValidator<T>
    {
        private readonly List<Func<T, ValidationError>> validators = new List<Func<T, ValidationError>>();

        public IValidator<T> AddRule(IValidationRule<T> rule)
        {
            validators.Add((T model) => rule.Validate(model) ? null : rule.Error);
            return this;
        }

        public ValidationResult Validate(T model) => new ValidationResult(validators.Select(validate => validate(model)).FirstOrDefault(error => error != null));
    }
}
