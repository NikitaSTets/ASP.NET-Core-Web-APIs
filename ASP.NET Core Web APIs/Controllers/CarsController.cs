using ASP.NET_Core_Web_APIs.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_Web_APIs.Controllers
{
    [Route("{controller}/{action}/{id}")]
    public class CarsController : ControllerBase
    {
        [HttpGet]
        public Car GetById(int id)
        {
            return new Car
            {
                Id = id,
                MakeName = "Chevrolet",
                ModelName = "Camaro"
            };
        }
    }
}