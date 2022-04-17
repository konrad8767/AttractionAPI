﻿using AttractionAPI.Entities;
using AttractionAPI.Models;
using AttractionAPI.Services;
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
        private readonly IAttractionService _attractionService;

        public AttractionControler(IAttractionService attractionService)
        {
            this._attractionService = attractionService;
        }

        [HttpPut("{attractionId}")]
        public ActionResult UpdateAttraction([FromRoute] int attractionId, [FromBody] UpdateAttractionDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isEdited = _attractionService.UpdateAttraction(attractionId, dto);

            if (isEdited)
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpDelete("{attractionId}")]
        public ActionResult DeleteAttraction([FromRoute] int attractionId)
        {
            var isDeleted = _attractionService.DeleteAttraction(attractionId);

            if (isDeleted)
            {
                return NoContent();
            }
            
            return NotFound();
        }

        [HttpPost]
        public ActionResult CreateAttraction([FromBody]CreateAttractionDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
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
        public ActionResult<AttractionDto> Get([FromRoute]int attractionId)
        {
            var attractionDto = _attractionService.GetById(attractionId);

            if (attractionDto is null)
            {
                return NotFound();
            }

            return Ok(attractionDto);
        }
    }
}
