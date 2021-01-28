using System.Collections.Generic;
using ASP.NET_Core_Web_APIs.Models;
using ASP.NET_Core_Web_APIs.Repositories.Interfaces;
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
        private readonly IRepository<Car> _carsRepository;


        public CarsController(
            IModelNameValidator modelNameValidator, 
            IMakeNameValidator makeNameValidator,
            IRepository<Car> carsRepository)
        {
            _modelNameValidator = modelNameValidator;
            _makeNameValidator = makeNameValidator;
            _carsRepository = carsRepository;
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
            var oldCar = _carsRepository.GetById(id);
            if (oldCar == null)
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
            var oldCar = _carsRepository.GetById(carId);
            if (oldCar == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public ActionResult<Car> Create([FromBody] Car car)
        {
            var existCar = _carsRepository.GetFirst(c => c.MakeName == car.MakeName && c.ModelName == car.ModelName);
            if (existCar != null)
            {
                return Conflict("Car already exist");
            }
            car.Id = 15;

            return CreatedAtAction(nameof(GetById), new { id = car.Id }, car);
        }

        [HttpPut("{carId}")]
        public ActionResult Update(int carId, Car newCar)
        {
            if (carId != newCar.Id)
            {
                return BadRequest("Id in url and body are different");
            }
            var oldCar = _carsRepository.GetById(carId);
            if (oldCar == null)
            {
                return NotFound();
            }

            var modelNameValidationResult = _modelNameValidator.Validate(newCar.ModelName);
            if (!modelNameValidationResult.IsSuccessful)
            {
                ModelState.AddModelError(nameof(newCar.ModelName), modelNameValidationResult.Message);
            }
            var makeNameValidationResult = _makeNameValidator.Validate(newCar.MakeName);
            if (!makeNameValidationResult.IsSuccessful)
            {
                ModelState.AddModelError(nameof(newCar.ModelName), makeNameValidationResult.Message);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _carsRepository.Update(newCar);

            return Ok();
        }
    }
}