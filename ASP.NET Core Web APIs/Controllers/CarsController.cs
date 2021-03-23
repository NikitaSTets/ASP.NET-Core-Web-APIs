using System.Collections.Generic;
using System.Net;
using ASP.NET_Core_Web_APIs.Errors;
using ASP.NET_Core_Web_APIs.Models;
using ASP.NET_Core_Web_APIs.Repositories.Interfaces;
using ASP.NET_Core_Web_APIs.Services.Interfaces;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_Web_APIs.Controllers
{
    [ApiController]
    [Route("api/cars")]
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


        /// <summary>
        /// //Cars return LOLOLO
        /// </summary>
        /// <returns> asdsa</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Car>> Cars()
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

        [HttpPost]
        public ActionResult<Car> Create(Car car)
        {
            var existCar = _carsRepository.GetFirst(c => c.MakeName == car.MakeName && c.ModelName == car.ModelName);
            if (existCar != null)
            {
                var error = new Error("Car already exist", ErrorCodes.AlreadyExist);

                throw new ApiException(error, (int)HttpStatusCode.Conflict);
            }
            car.Id = 15;

            return CreatedAtAction(nameof(GetById), new { id = car.Id }, car);
        }

        [HttpPut("{carId}")]
        public ActionResult Update(int carId, Car newCar)
        {
            if (carId != newCar.Id)
            {
                var error = new Error("Id in url and body are different", ErrorCodes.IdInUrlAndBodyAreDifferent, nameof(carId));

                throw new ApiException(error, (int)HttpStatusCode.Conflict);
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

        [HttpDelete("{carId}")]
        public ActionResult Delete(int carId)
        {
            var oldCar = _carsRepository.GetById(carId);
            if (oldCar == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}