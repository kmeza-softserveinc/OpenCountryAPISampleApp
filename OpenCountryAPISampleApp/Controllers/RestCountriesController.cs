using Microsoft.AspNetCore.Mvc;
using OpenCountryAPISampleApp.Services;
using System.Net;
using Newtonsoft.Json;
using System;
using OpenCountryAPISampleApp.Helpers;
using OpenCountryAPISampleApp.Interfaces.IServices;

namespace OpenCountryAPISampleApp.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class RestCountriesController : ControllerBase
    {
        private readonly IRestCountriesService _restCountriesService;

        public RestCountriesController(IRestCountriesService restCountriesService)
        {
            _restCountriesService = restCountriesService;
        }

        [HttpGet]
        [Route("/country")]
        public async Task<IActionResult> Get(string? population = null, string? name = null, string? pagination = null, string? sort = null)
        {
            int paginationN;
            if (string.IsNullOrEmpty(sort))
            {
                sort = "";
            }

            if (string.IsNullOrEmpty(pagination))
            {
                paginationN = 0;
            }
            else
            {
                if (!int.TryParse(pagination, out paginationN))
                {
                    return new ObjectResult(new { error = "Give a Valid Number to paginate!" })
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest
                    };
                }
            }
            
            if (!string.IsNullOrEmpty(population) && string.IsNullOrEmpty(name))
            {
                var result = await _restCountriesService.GetCountriesByPopulationAsync(population, paginationN, sort);
                return Ok(JsonConvert.SerializeObject(result));
            }

            else if (string.IsNullOrEmpty(population) && !string.IsNullOrEmpty(name))
            {
                var result = await _restCountriesService.GetCountriesByNameAsync(name, paginationN, sort);
                return Ok(JsonConvert.SerializeObject(result));
            }
            
            else if (!string.IsNullOrEmpty(population) && !string.IsNullOrEmpty(name))
            {
                var result = await _restCountriesService.GetCountriesByNameAndPopulationAsync(name, population, paginationN, sort);
                return Ok(JsonConvert.SerializeObject(result));
            }

            else
            {
                var result = await _restCountriesService.GetAllCountriesAsync(paginationN, sort);
                return Ok(JsonConvert.SerializeObject(result));
            }

        }

        [HttpPost]
        [Route("/country")]
        public IActionResult Post()
        {
            return new ObjectResult(new { error = "Not implemented yet" })
            {
                StatusCode = (int)HttpStatusCode.NotImplemented
            };
        }

        [HttpPut]
        [Route("/country")]
        public IActionResult Put()
        {
            return new ObjectResult(new { error = "Not implemented yet" })
            {
                StatusCode = (int)HttpStatusCode.NotImplemented
            };
        }

        [HttpDelete]
        [Route("/country")]
        public IActionResult Delete()
        {
            return new ObjectResult(new { error = "Not implemented yet" })
            {
                StatusCode = (int)HttpStatusCode.NotImplemented
            };
        }

    }
}
