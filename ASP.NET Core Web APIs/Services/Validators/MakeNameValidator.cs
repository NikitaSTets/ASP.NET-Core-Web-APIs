using System.Linq;
using ASP.NET_Core_Web_APIs.Services.Interfaces;

namespace ASP.NET_Core_Web_APIs.Services.Validators
{
    public class MakeNameValidator : IMakeNameValidator
    {
        private static readonly string[] MakeNames = {
            "Chevrolet", "Audi", "Reno", "Volvo", "BMW", "Skoda"
        };

        public OperationResult<bool> Validate(string makeName)
        {
            var isValidMakeName = MakeNames.Contains(makeName);

            return isValidMakeName 
                ? OperationResult.CreateSuccessful(true)
                : OperationResult<bool>.CreateUnsuccessful($"Unknown {makeName} makeName");
        }
    }
}