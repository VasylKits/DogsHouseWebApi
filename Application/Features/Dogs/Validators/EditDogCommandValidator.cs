using Application.Features.Dogs.Commands.Edit;
using FluentValidation;

namespace Application.Features.Dogs.Validators;

public class EditDogCommandValidator : AbstractValidator<EditDogCommand>
{
	public EditDogCommandValidator()
	{
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Name)
            .MaximumLength(20)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Color)
            .MaximumLength(20)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.TailLength)
            .GreaterThan(0)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Weight)
            .GreaterThan(0)
            .NotNull()
            .NotEmpty();
    }
}