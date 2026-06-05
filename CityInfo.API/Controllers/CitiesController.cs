using AutoMapper;
using CityInfo.API.Entities;
using CityInfo.API.Models;
using CityInfo.API.Repositories;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Threading.Tasks;

namespace CityInfo.API.Controllers
{

    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        public CitiesController(ICityRepository cityRepository, IMapper mapper , IEmailService emailService)
        {
            _cityRepository = cityRepository ?? throw new ArgumentNullException(nameof(cityRepository));
            _mapper = mapper;
            _emailService = emailService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutPointDto>>> GetCities()
        {
            var cities = await _cityRepository.GetCitiesAsync();
            var mapped = _mapper.Map<IEnumerable<CityWithoutPointDto>>(cities);
            return Ok(mapped);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCity(int id, bool includePointOfInterests)
         {
            var city = await _cityRepository.GetCityAsync(id, includePointOfInterests);
            if (city == null)
            {
                return NotFound();
            }

            if (includePointOfInterests)
            {
                return Ok(_mapper.Map<CityDto>(city));
            }

            //var body = JsonSerializer.Serialize(city);
            //await _emailService.SendEmailAsync("bakhtiarimoghadamali@yahoo.com","test",body);
       
            return Ok(_mapper.Map<CityWithoutPointDto>(city));
        }
    }
}
