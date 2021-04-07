using System.Collections.Generic;
using ASP.NET_Core_Web_APIs.Constants;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_Web_APIs.Controllers
{
    [ApiController]
    [ApiVersion(Api.Versions.V1)]
    [ApiVersion(Api.Versions.V2)]
    [Route("api/v{version:apiVersion}/cars/[controller]")]
    public class MakeNamesController : ControllerBase
    {
        [HttpGet]
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
