using System.Collections.Generic;
using ASP.NET_Core_Web_APIs.Constants;
using ASP.NET_Core_Web_APIs.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_Web_APIs.Controllers
{
    [ApiController]
    [ApiVersion(Api.Versions.V2)]
    [Route("api/v{version:apiVersion}/cars/{carId}")]
    [ApiExplorerSettings(GroupName = Api.Groups.CarsGroupName)]
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