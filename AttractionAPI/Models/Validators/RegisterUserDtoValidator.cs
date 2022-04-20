using AttractionAPI.Entities;
using FluentValidation;
using System.Linq;

namespace AttractionAPI.Models.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(AttractionDbContext dbContext)
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).MinimumLength(6);
            RuleFor(x => x.ConfirmPassowrd).Equal(x => x.Password);
            RuleFor(x => x.Email).Custom((value, context) =>
            {
                var emailInUse = dbContext.Users.Any(x => x.Email == value);
                if (emailInUse)
                {
                    context.AddFailure("Email", "That email is already in use.");
                }
            });
            RuleFor(x => x.Name).Custom((value, context) =>
            {
                var nameInUse = dbContext.Users.Any(x => x.Name == value);
                if (nameInUse)
                {
                    context.AddFailure("Name", "That name is already in use.");
                }
            });
        }
    }
}
