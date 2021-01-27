using System.Linq;
using ASP.NET_Core_Web_APIs.Services.Interfaces;

namespace ASP.NET_Core_Web_APIs.Services.Validators
{
    public class ModelNameValidator : IModelNameValidator
    {
        private static readonly string[] ModelNames = {
            "Comaro", "A6", "A4", "80", "i3", "Rapid"
        };

        public bool Validate(string modelName)
        {
            var isValidModelName = ModelNames.Contains(modelName);

            return isValidModelName;
        }
    }
}