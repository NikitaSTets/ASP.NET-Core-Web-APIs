using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using ASP.NET_Core_Web_APIs.Constants;
using ASP.NET_Core_Web_APIs.Errors;
using ASP.NET_Core_Web_APIs.Models;
using ASP.NET_Core_Web_APIs.Repositories.Interfaces;
using ASP.NET_Core_Web_APIs.Services.Interfaces;
using AutoWrapper.Extensions;
using AutoWrapper.Models;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ASP.NET_Core_Web_APIs.Controllers
{
    [ApiController]
    [ApiVersion(Api.Versions.V1)]
    [Route("api/v{version:apiVersion}/[controller]")]
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
        [EnableCors(PolicyName = PolicyConstants.MyAllowSpecificOrigins)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Car>))]
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Car))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Car))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(Error))]
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
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiProblemDetailsValidationErrorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(Error))]
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
                var errors = ModelState.AllErrors();

                throw new ApiException(errors, (int)HttpStatusCode.BadRequest);
            }

            _carsRepository.Update(newCar);

            return Ok();
        }

        [HttpDelete("{carId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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