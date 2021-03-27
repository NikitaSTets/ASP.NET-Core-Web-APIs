using System.Collections.Generic;
using ASP.NET_Core_Web_APIs.Constants;
using ASP.NET_Core_Web_APIs.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_Web_APIs.Controllers
{
    [ApiController]
    [Route("api/cars/{carId}")]
    [ApiExplorerSettings(GroupName = ApiVersions.V2)]
    public class OwnersController : ControllerBase
    {
        [HttpGet("owners")]
        public ActionResult<List<Person>> Owners(int carId)
        {
            return new List<Person>
            {
                new Person
                {
                    Name = "name",
                    SurName = "surName",
                    CarId = carId
                },
                new Person
                {
                    Name = "newName",
                    SurName = "surName",
                    CarId = carId
                }
            };
        }
    }
}