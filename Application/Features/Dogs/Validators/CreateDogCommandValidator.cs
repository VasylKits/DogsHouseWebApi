using Application.Features.Dogs.Commands.Create;
using FluentValidation;

namespace Application.Features.Dogs.Validators;

public class CreateDogCommandValidator : AbstractValidator<CreateDogCommand>
{
    public CreateDogCommandValidator()
    {
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