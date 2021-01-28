namespace ASP.NET_Core_Web_APIs.Services.Interfaces
{
    public interface IModelNameValidator
    {
        public OperationResult<bool> Validate(string modelName);
    }
}