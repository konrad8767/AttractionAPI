using AttractionAPI.Entities;
using AttractionAPI.Exceptions;
using AttractionAPI.Models;
using AutoMapper;
using System.Linq;

namespace AttractionAPI.Services
{
    public interface ICommentService
    {
        int CreateComment(int attractionId, CreateCommentDto dto);
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
            //var attraction = _context.Attractions.FirstOrDefault(x => x.Id == attractionId);

            if (_context.Attractions.FirstOrDefault(x => x.Id == attractionId) is null)
            {
                throw new NotFoundException("Attraction not found.");
            }

            var existingUser = _context.Users.FirstOrDefault(x => x.Name == dto.UserName);

            if (existingUser is null)
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
    }
}
