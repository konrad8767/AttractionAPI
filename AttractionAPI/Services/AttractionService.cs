using AttractionAPI.Entities;
using AttractionAPI.Exceptions;
using AttractionAPI.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace AttractionAPI.Services
{
    public interface IAttractionService
    {
        int CreateAttraction(CreateAttractionDto dto);
        void DeleteAttraction(int attractionId);
        void UpdateAttraction(int attractionId, UpdateAttractionDto dto);
        IEnumerable<AttractionDto> GetAll();
        AttractionDto GetById(int attractionId);
    }

    public class AttractionService : IAttractionService
    {
        private readonly AttractionDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public AttractionService(AttractionDbContext dbContext, IMapper mapper, ILogger<AttractionService> logger)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
            this._logger = logger;
        }

        public void DeleteAttraction(int attractionId)
        {
            _logger.LogWarning($"Attraction with id: {attractionId} DELETE action invoked.");
            var attraction = _dbContext
                .Attractions
                .FirstOrDefault(x => x.Id == attractionId);

            if (attraction == null)
            {
                throw new NotFoundException("Attraction not found.");
            }

            _dbContext.Attractions.Remove(attraction);
            _dbContext.SaveChanges();
        }

        public void UpdateAttraction(int attractionId, UpdateAttractionDto dto)
        {
            var attraction = _dbContext
                .Attractions
                .FirstOrDefault(x => x.Id == attractionId);

            if (attraction is null)
            {
                throw new NotFoundException("Attraction not found.");
            }

            attraction.Name = dto.Name;
            attraction.Description = dto.Description;
            attraction.Category = dto.Category;

            _dbContext.Attractions.Update(attraction);
            _dbContext.SaveChanges();
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
                throw new NotFoundException("Attraction not found.");
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
