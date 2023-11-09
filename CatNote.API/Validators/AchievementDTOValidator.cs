using CatNote.API.DTO;
using FluentValidation;

namespace CatNote.API.Validotors;

public class AchievementDTOValidator : AbstractValidator<AchievementDTO>
{
    public AchievementDTOValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .NotNull()
            .WithMessage("Title Required");

        RuleFor(x => x.Description)
            .NotEmpty()
            .NotNull()
            .WithMessage("Description Required");
    }
}
