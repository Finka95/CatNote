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
            .WithMessage("Title Required");

        RuleFor(x => x.Status)
            .NotEmpty()
            .NotNull()
            .WithMessage("Status Required");

        RuleFor(x => x.Date)
            .NotEmpty()
            .NotNull()
            .WithMessage("Date Required");
    }
}
