using AttractionAPI.Entities;
using AttractionAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace AttractionAPI.Services
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);
    }

    public class AccountService : IAccountService
    {
        private readonly AttractionDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        public AccountService(AttractionDbContext dbContext, IPasswordHasher<User> passwordHasher)
        {
            this._dbContext = dbContext;
            this._passwordHasher = passwordHasher;
        }
        public void RegisterUser(RegisterUserDto dto)
        {
            var newUser = new User()
            {
                Name = dto.Name,
                Email = dto.Email,
                RoleId = dto.RoleId
            };
            var hashedPassowrd = _passwordHasher.HashPassword(newUser, dto.Password);
            newUser.PasswordHash = hashedPassowrd;

            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();
        }
    }
}
