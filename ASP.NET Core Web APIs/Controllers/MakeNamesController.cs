using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_Web_APIs.Controllers
{
    [Route("api/cars/[controller]")]
    [ApiController]
    public class MakeNamesController : ControllerBase
    {
        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 300)]
        public IEnumerable<string> GetCarMakeNames()
        {
            return new List<string>
            {
                "Reno",
                "Audi",
                "BMW",
                "Chevrolet"
            };
        }
    }
}
