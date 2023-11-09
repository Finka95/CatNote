using CatNote.API.DTO;
using FluentValidation;

namespace CatNote.API.Validators;

public class TaskDTOValidator : AbstractValidator<TaskDTO>
{
    public TaskDTOValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .NotNull()
            .WithMessage("Description Required");

        RuleFor(x => x.Status)
            .NotEmpty()
            .NotNull()
            .WithMessage("Description Required");

        RuleFor(x => x.Date)
            .NotEmpty()
            .NotNull()
            .WithMessage("Description Required");
    }
}
