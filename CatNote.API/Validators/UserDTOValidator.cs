using CatNote.API.DTO;
using FluentValidation;

namespace CatNote.API.Validators;

public class UserDTOValidator : AbstractValidator<UserDTO>
{
    public UserDTOValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .NotNull();
    }
}
