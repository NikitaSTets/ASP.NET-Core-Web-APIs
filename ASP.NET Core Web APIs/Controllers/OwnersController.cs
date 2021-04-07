using System.Collections.Generic;
using ASP.NET_Core_Web_APIs.Constants;
using ASP.NET_Core_Web_APIs.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_Web_APIs.Controllers
{
    [ApiController]
    [ApiVersion(Api.Versions.V1)]
    [ApiVersion(Api.Versions.V2)]
    [Route("api/v{version:apiVersion}/cars/{carId}")]
    [ApiExplorerSettings(GroupName = Api.Groups.CarsGroupName)]
    public class OwnersController : ControllerBase
    {
        [HttpGet]
        [MapToApiVersion(Api.Versions.V1)]
        [Route("owners")]
        public ActionResult<List<Person>> Owners1(int carId)
        {
            return new List<Person>
            {
                new Person
                {
                    Name = "namev1",
                    SurName = "surNamev1",
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

        [HttpGet]
        [MapToApiVersion(Api.Versions.V2)]
        [Route("owners")]
        public ActionResult<List<Person>> Owners2(int carId)
        {
            return new List<Person>
            {
                new Person
                {
                    Name = "namev2",
                    SurName = "surNamev2",
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