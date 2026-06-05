using AutoMapper;
using CityInfo.API.Entities;
using CityInfo.API.Models;
using CityInfo.API.Repositories;
using CityInfo.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities/{cityId}/Interests")]
    public class InterestsController : ControllerBase
    {
        private readonly ILogger<InterestsController> _logger;
        private readonly ISmsService _smsService;
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;
        public InterestsController(ILogger<InterestsController> logger ,
            ISmsService smsService, ICityRepository cityRepository , IMapper mapper)
        {
            _logger = logger;
            _smsService = smsService;
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        #region Get

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointsOfInterest(int cityId)
        {
            if (!await _cityRepository.IsCityExistAsync(cityId))
            {
                _logger.LogInformation($"city with id {cityId} not found");
                return NotFound();
            }
            var points = await _cityRepository.GetPointsOfInterestsForCityAsync(cityId);
            return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(points));
        }



        [HttpGet("{pointOfInterestId}")]
        public async Task<ActionResult<PointOfInterestDto>> GetpointOfInterest(int cityId, int pointOfInterestId)
        {
            if (!await _cityRepository.IsCityExistAsync(cityId))
            {
                _logger.LogInformation($"city with id {cityId} not found");
                return NotFound();
            }

            var point = await _cityRepository.GetPointOfInterestAsync(cityId, pointOfInterestId);
            if (point == null)
            {
                return NotFound();
            }
            return _mapper.Map<PointOfInterestDto>(point);
        }

        #endregion

        #region Post

        [HttpPost]
        public async Task<ActionResult<PointOfInterestDto>> CreatePointOfInterest(int cityId, PointOfInterestForCreationDto pointDto)
        {
            if (!await _cityRepository.IsCityExistAsync(cityId))
            {
                return NotFound();
            }
            var pointOfInterest = _mapper.Map<PointOfInterest>(pointDto);
            await _cityRepository.AddPointOfInterestAsync(cityId, pointOfInterest);
            await _cityRepository.SaveChangesAsync();

            var createdPointDto = _mapper.Map<PointOfInterestDto>(pointOfInterest); 
            return CreatedAtAction("GetpointOfInterest", new
            {
                cityId = cityId ,
                pointOfInterestId = createdPointDto.Id
            },createdPointDto);
        }

        #endregion

        #region Put

        [HttpPut("{pointId}")]
        public async Task<ActionResult> UpdatePointOfInterest(int cityId ,
            int pointId , PointOfInterestForUpdateDto point)
        {
            var pointOfInterest =  await _cityRepository.GetPointOfInterestAsync(cityId, pointId);

            if (pointOfInterest == null)
            {
                return NotFound();
            }

            _mapper.Map(point, pointOfInterest);
            await _cityRepository.SaveChangesAsync();
            return NoContent();
        }

        #endregion

        #region Patch
        [HttpPatch("{pointId}")]
        public async Task<ActionResult> PatchUpdate(int cityId , int pointId , JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)
        {
            var pointOfInterest = await _cityRepository.GetPointOfInterestAsync(cityId, pointId);
            if (pointOfInterest == null)
            {
                return NotFound();
            }
            var pointToPatch = _mapper.Map<PointOfInterestForUpdateDto>(pointOfInterest);

            patchDocument.ApplyTo(pointToPatch, ModelState);
            TryValidateModel(pointToPatch);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _mapper.Map(pointToPatch, pointOfInterest);
            await _cityRepository.SaveChangesAsync();
            return NoContent();
        }

        #endregion

        #region Delete

        [HttpDelete("{pointId}")]
        public async Task<ActionResult> DeletePoint(int cityId, int pointId)
        {
            var pointOfInterest = await _cityRepository.GetPointOfInterestAsync(cityId, pointId);
            if (pointOfInterest == null)
            {
                return NotFound();
            }
            _cityRepository.DeletePointOfInterest(pointOfInterest);
            await _cityRepository.SaveChangesAsync();
            return NoContent();
        }
        #endregion
    }
}
