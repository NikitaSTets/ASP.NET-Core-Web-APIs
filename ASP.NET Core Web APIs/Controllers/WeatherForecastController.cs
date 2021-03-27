using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using ASP.NET_Core_Web_APIs.Constants;
using ASP.NET_Core_Web_APIs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ASP.NET_Core_Web_APIs.Controllers
{
    [ApiController]
    [ApiVersion(ApiVersions.V1)]
    [ApiExplorerSettings(GroupName = ApiVersions.V1GroupName)]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpPost]
        public ActionResult<WeatherForecast> Create(WeatherForecast weatherForecast)
        {
            //var a = new ModelStateDictionary();
            //a.AddModelError("key", "error message");

            //return ValidationProblem(a);

            ModelState.AddModelError("key", "error message");

            return ValidationProblem();
        }

        [HttpPost]
        public ActionResult CreateNew(WeatherForecast weatherForecast)
        {
            return NotFound();
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Xml)]
        public IActionResult CreateWeatherForecast(WeatherForecast weatherForecast)
        {
            return CreatedAtAction(nameof(GetById), new { id = weatherForecast.Id }, weatherForecast);
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> GetById()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
        }
    }
}