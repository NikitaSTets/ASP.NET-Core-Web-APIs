using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ASP.NET_Core_Web_APIs.Models;
using ASP.NET_Core_Web_APIs.Repositories;
using ASP.NET_Core_Web_APIs.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_Web_APIs.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class CarsController : ControllerBase
    {
        private readonly IModelNameValidator _modelNameValidator;
        private readonly IMakeNameValidator _makeNameValidator;


        public CarsController(IModelNameValidator modelNameValidator, IMakeNameValidator makeNameValidator)
        {
            _modelNameValidator = modelNameValidator;
            _makeNameValidator = makeNameValidator;
        }


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
        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 300)]
        public ActionResult<Car> GetById(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            return new Car
            {
                Id = id,
                MakeName = "Chevrolet",
                ModelName = "Camaro"
            };
        }

        [HttpGet("makeNames")]
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

        [HttpGet("{carId}/{action}")]
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

        [HttpDelete("{carId}")]
        public ActionResult DeleteCar(int carId)
        {
            return NoContent();
        }

        [HttpPost]
        public ActionResult<Car> Create([FromBody] Car car)
        {
            car.Id = 15;

            return CreatedAtAction(nameof(GetById), new { id = car.Id }, car);
        }

        [HttpPut("{carId}")]
        public ActionResult Update(int carId, Car newCar)
        {
            if (carId != newCar.Id)
            {
                BadRequest();
            }
            var carsRepository = new CarsRepository();

            var oldCar = carsRepository.GetById(carId);
            if (oldCar == null)
            {
                return NotFound();
            }
            var isModelNameValid = _modelNameValidator.Validate(newCar.ModelName);
            if (!isModelNameValid)
            {
                ModelState.AddModelError(nameof(newCar.ModelName),$"Unsupported {nameof(newCar.ModelName)}");
            }
            var isMakeNameValid = _makeNameValidator.Validate(newCar.MakeName);
            if (!isMakeNameValid)
            {
                ModelState.AddModelError(nameof(newCar.ModelName), $"Unsupported {nameof(newCar.ModelName)}");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            carsRepository.Update(newCar);

            return Ok();
        }
    }
}