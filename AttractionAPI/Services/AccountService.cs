using AttractionAPI.Entities;
using AttractionAPI.Exceptions;
using AttractionAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace AttractionAPI.Services
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);
        string GenerateJwt(LoginDto dto);
    }

    public class AccountService : IAccountService
    {
        private readonly AttractionDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        public AccountService(AttractionDbContext dbContext, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            this._dbContext = dbContext;
            this._passwordHasher = passwordHasher;
            this._authenticationSettings = authenticationSettings;
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

        public string GenerateJwt(LoginDto dto)
        {
            var user = _dbContext
                .Users
                .Include(x => x.Role)
                .FirstOrDefault(x => x.Email == dto.Email);

            if (user is null)
            {
                throw new BadRequestException("Invalid username or password.");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid username or password.");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.Name}"),
                new Claim(ClaimTypes.Role, $"{user.Role.Name}")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
