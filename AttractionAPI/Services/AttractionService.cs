using AttractionAPI.Entities;
using AttractionAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AttractionAPI.Services
{
    public interface IAttractionService
    {
        int CreateAttraction(CreateAttractionDto dto);
        bool DeleteAttraction(int attractionId);
        bool UpdateAttraction(int attractionId, UpdateAttractionDto dto);
        IEnumerable<AttractionDto> GetAll();
        AttractionDto GetById(int attractionId);
    }

    public class AttractionService : IAttractionService
    {
        private readonly AttractionDbContext _dbContext;
        private readonly IMapper _mapper;

        public AttractionService(AttractionDbContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        public bool DeleteAttraction(int attractionId)
        {
            var attraction = _dbContext
                .Attractions
                .FirstOrDefault(x => x.Id == attractionId);

            if (attraction == null)
            {
                return false;
            }

            _dbContext.Attractions.Remove(attraction);
            _dbContext.SaveChanges();

            return true;
        }

        public bool UpdateAttraction(int attractionId, UpdateAttractionDto dto)
        {
            var attraction = _dbContext
                .Attractions
                .FirstOrDefault(x => x.Id == attractionId);

            if (attraction is null)
            {
                return false;
            }

            attraction.Name = dto.Name;
            attraction.Description = dto.Description;
            attraction.Category = dto.Category;

            _dbContext.Attractions.Update(attraction);
            _dbContext.SaveChanges();

            return true;
        }

        public AttractionDto GetById(int attractionId)
        {
            var attraction = _dbContext
                .Attractions
                .Include(x => x.Address)
                .Include(x => x.Comments)
                .FirstOrDefault(x => x.Id == attractionId);

            if (attraction is null)
            {
                return null;
            }

            var attractionDto = _mapper.Map<AttractionDto>(attraction);
            return attractionDto;
        }

        public IEnumerable<AttractionDto> GetAll()
        {
            var attractions = _dbContext
                .Attractions
                .Include(x => x.Address)
                .Include(x => x.Comments)
                .ToList();

            var attractionsDto = _mapper.Map<IEnumerable<AttractionDto>>(attractions);

            return attractionsDto;
        }

        public int CreateAttraction(CreateAttractionDto dto)
        {
            var attraction = _mapper.Map<Attraction>(dto);
            _dbContext.Attractions.Add(attraction);
            _dbContext.SaveChanges();

            return attraction.Id;
        }
    }
}
