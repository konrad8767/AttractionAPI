using AttractionAPI.Entities;
using AttractionAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AttractionAPI.Controllers
{
    [Route("api/attraction")]
    public class AttractionControler : ControllerBase
    {
        private readonly AttractionDbContext _dbContext;
        private readonly IMapper _mapper;

        public AttractionControler(AttractionDbContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        [HttpPost]
        public ActionResult CreateAttraction([FromBody]CreateAttractionDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var attraction = _mapper.Map<Attraction>(dto);
            _dbContext.Attractions.Add(attraction);
            _dbContext.SaveChanges();

            return Created($"/api/attraction/{attraction.Id}", null);
        }

        [HttpGet]
        public ActionResult<IEnumerable<AttractionDto>> GetAll()
        {
            var attractions = this._dbContext
                .Attractions
                .Include(x => x.Address)
                .Include(x => x.Comments)
                .ToList();

            var attractionsDtos = _mapper.Map<List<AttractionDto>>(attractions);

            return Ok(attractionsDtos);
        }

        [HttpGet("{attractionid}")]
        public ActionResult<AttractionDto> Get([FromRoute]int attractionId)
        {
            var attraction = _dbContext
                .Attractions
                .Include(x => x.Address)
                .Include(x => x.Comments)
                .FirstOrDefault(x => x.Id == attractionId);

            if (attraction is null)
            {
                return NotFound();
            }

            var attractionDto = _mapper.Map<AttractionDto>(attraction);
            return Ok(attractionDto);
        }
    }
}
