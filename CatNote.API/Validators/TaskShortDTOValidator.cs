using CatNote.API.DTO;
using FluentValidation;

namespace CatNote.API.Validators;

public class TaskShortDTOValidator : AbstractValidator<TaskShortDTO>
{
    public TaskShortDTOValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .NotNull()
            .WithMessage("Description Required");
    }
}
