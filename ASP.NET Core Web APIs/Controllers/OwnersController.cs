﻿using System.Collections.Generic;
using ASP.NET_Core_Web_APIs.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_Web_APIs.Controllers
{
    [Route("api/cars/{carId}")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        [HttpGet("{action}")]
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