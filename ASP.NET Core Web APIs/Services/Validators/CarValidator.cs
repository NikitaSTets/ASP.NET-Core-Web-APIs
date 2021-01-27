using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ASP.NET_Core_Web_APIs.Models;
using ASP.NET_Core_Web_APIs.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ASP.NET_Core_Web_APIs.Services.Validators
{
    public class CarValidator : ICarValidator, IValidatableObject
    {
        private readonly IMakeNameValidator _makeNameValidator;
        private readonly IModelNameValidator _modelNameValidator;


        public CarValidator(IMakeNameValidator makeNameValidator, IModelNameValidator modelNameValidator)
        {
            _makeNameValidator = makeNameValidator;
            _modelNameValidator = modelNameValidator;
        }


        public bool Validate(Car car)
        {
            var isModelNameValid = _modelNameValidator.Validate(car.ModelName);
            var isMakeNameValid = _makeNameValidator.Validate(car.MakeName);

            return isMakeNameValid && isModelNameValid;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext context)
        {
            var errors = new List<ValidationResult>();

            var car = context.ObjectInstance as Car;
            var isModelNameValid = _modelNameValidator.Validate(car.ModelName);
            if (!isModelNameValid)
            {
                var modelNameValidationResult = new ValidationResult($"Unsupported {nameof(car.ModelName)}");
                errors.Add(modelNameValidationResult);
            }
            var isMakeNameValid = _makeNameValidator.Validate(car.MakeName);
            if (!isMakeNameValid)
            {
                var makeNameValidationResult = new ValidationResult($"Unsupported {nameof(car.MakeName)}");
                errors.Add(makeNameValidationResult);
            }

            return errors;
        }
    }
}