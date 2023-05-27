using CarWorkshop.Application.CQRS.CarWorkshopServices.Commands;
using FluentValidation;

namespace CarWorkshop.Application.Validation;

public class CreateCarWorkshopServiceCommandValidator : AbstractValidator<CreateCarWorkshopServiceCommand>
{
    public CreateCarWorkshopServiceCommandValidator()
    {
        RuleFor(x => x.Cost)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.Description)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.CarWorkshopEncodedName)
            .NotEmpty()
            .NotNull();
    }
}
