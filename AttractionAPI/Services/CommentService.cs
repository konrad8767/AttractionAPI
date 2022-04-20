using AttractionAPI.Entities;
using AttractionAPI.Exceptions;
using AttractionAPI.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AttractionAPI.Services
{
    public interface ICommentService
    {
        int CreateComment(int attractionId, CreateCommentDto dto);
        CommentDto GetById(int attractionId, int commentId);
        List<CommentDto> GetAll(int attractionId);
        void Remove(int attractionId, int commentId);
        void RemoveAll(int attractionId);
    }

    public class CommentService : ICommentService
    {
        private readonly AttractionDbContext _context;
        private readonly IMapper _mapper;
        public CommentService(AttractionDbContext contex, IMapper mapper)
        {
            this._context = contex;
            this._mapper = mapper;
        }

        public int CreateComment(int attractionId, CreateCommentDto dto)
        {
            if (_context.Attractions.FirstOrDefault(x => x.Id == attractionId) is null)
            {
                throw new NotFoundException("Attraction not found.");
            }

            var existingUser = _context.Users.FirstOrDefault(x => x.Name == dto.UserName);

            if (existingUser is null) // fixme
            {
                var newCreatedUser = new CreateUserDto() { Name = dto.UserName };
                var newUser = _mapper.Map<User>(newCreatedUser);
                _context.Users.Add(newUser);
                _context.SaveChanges();

                var commentEntity = _mapper.Map<Comment>(dto);
                commentEntity.AttractionId = attractionId;
                commentEntity.UserId = newUser.Id;
                _context.Comments.Add(commentEntity);
                _context.SaveChanges();

                return commentEntity.Id;
            }
            else
            {
                var commentEntity = _mapper.Map<Comment>(dto);
                commentEntity.AttractionId = attractionId;
                commentEntity.UserId = existingUser.Id;
                _context.Comments.Add(commentEntity);
                _context.SaveChanges();

                return commentEntity.Id;
            }
        }

        public CommentDto GetById(int attractionId, int commentId)
        {
            if (_context.Attractions.FirstOrDefault(x => x.Id == attractionId) is null)
            {
                throw new NotFoundException("Attraction not found.");
            }

            var comment = _context.Comments.FirstOrDefault(x => x.Id == commentId);
            if (comment is null || comment.AttractionId != attractionId)
            {
                throw new NotFoundException("Comment not found.");
            }

            var commentDto = _mapper.Map<CommentDto>(comment);
            return commentDto;
        }

        public List<CommentDto> GetAll(int attractionId)
        {
            var attraction = GetAttractionById(attractionId);

            var commentDtos = _mapper.Map<List<CommentDto>>(attraction.Comments);
            return commentDtos;

        }


        public void Remove(int attractionId, int commentId)
        {
            var comment = _context
                .Comments
                .FirstOrDefault(x => x.Id == commentId);

            if (comment == null || comment.AttractionId != attractionId)
            {
                throw new NotFoundException("Comment not found.");
            }

            _context.Comments.Remove(comment);
            _context.SaveChanges();
        }

        public void RemoveAll(int attractionId)
        {
            var attraction = GetAttractionById(attractionId);

            _context.Comments.RemoveRange(attraction.Comments);
            _context.SaveChanges();
        }

        private Attraction GetAttractionById(int attractionId)
        {
            var attraction = _context
                .Attractions
                .Include(x => x.Comments)
                .FirstOrDefault(x => x.Id == attractionId);

            if (attraction is null)
            {
                throw new NotFoundException("Attraction not found.");
            }

            return attraction;
        }
    }
}
