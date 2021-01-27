using ASP.NET_Core_Web_APIs.Models;

namespace ASP.NET_Core_Web_APIs.Services.Interfaces
{
    public interface ICarValidator
    {
        public bool Validate(Car car);
    }
}