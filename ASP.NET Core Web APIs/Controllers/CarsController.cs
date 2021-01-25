using System.Collections.Generic;
using ASP.NET_Core_Web_APIs.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_Web_APIs.Controllers
{
    [Route("{controller}")]
    public class CarsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Car>> CarsList()
        {
            return new List<Car>
            {
                new Car
                {
                    Id = 15,
                    MakeName = "Chevrolet",
                    ModelName = "Camaro"
                },
                new Car
                {
                    Id = 123,
                    MakeName = "Audi",
                    ModelName = "A6"
                }
        };
        }

        [HttpGet("{id}")]
        public Car GetById(int id)
        {
            return new Car
            {
                Id = id,
                MakeName = "Chevrolet",
                ModelName = "Camaro"
            };
        }

        [HttpGet("{carId}/{action}")]
        public ActionResult<List<People>> Owners(int carId)
        {
            return new List<People>
            {
                new People
                {
                    Name = "name",
                    SurName = "surName",
                    CarId = carId
                },
                new People
                {
                    Name = "newName",
                    SurName = "surName",
                    CarId = carId
                }
            };
        }

        [HttpDelete("{carId}")]
        public ActionResult DeleteCar(int carId)
        {
            return Ok();
        }

        [HttpPost("create")]
        public ActionResult CreateCar([FromBody]Car car)
        {
            car.Id = 15;

            return CreatedAtAction(nameof(GetById), new { id = car.Id }, car);
        }
    }
}