using AttractionAPI.Models;
using AttractionAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AttractionAPI.Controllers
{
    [Route("api/attraction")]
    [ApiController]
    public class AttractionController : ControllerBase
    {
        private readonly IAttractionService _attractionService;

        public AttractionController(IAttractionService attractionService)
        {
            this._attractionService = attractionService;
        }

        [HttpPut("{attractionId}")]
        public ActionResult UpdateAttraction([FromRoute] int attractionId, [FromBody] UpdateAttractionDto dto)
        {
            _attractionService.UpdateAttraction(attractionId, dto);

            return Ok();
        }

        [HttpDelete("{attractionId}")]
        public ActionResult DeleteAttraction([FromRoute] int attractionId)
        {
            _attractionService.DeleteAttraction(attractionId);

            return NoContent();
        }

        [HttpPost]
        public ActionResult CreateAttraction([FromBody] CreateAttractionDto dto)
        {
            var attractionId = _attractionService.CreateAttraction(dto);

            return Created($"/api/attraction/{attractionId}", null);
        }

        [HttpGet]
        public ActionResult<IEnumerable<AttractionDto>> GetAll()
        {
            var attractionsDtos = _attractionService.GetAll();

            return Ok(attractionsDtos);
        }

        [HttpGet("{attractionid}")]
        public ActionResult<AttractionDto> Get([FromRoute] int attractionId)
        {
            var attractionDto = _attractionService.GetById(attractionId);

            return Ok(attractionDto);
        }
    }
}
