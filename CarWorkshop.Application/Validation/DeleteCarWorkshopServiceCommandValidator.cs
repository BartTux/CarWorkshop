using FluentValidation;
using CarWorkshop.Application.CQRS.CarWorkshopServices.Commands;

namespace CarWorkshop.Application.Validation;

public class DeleteCarWorkshopServiceCommandValidator : AbstractValidator<DeleteCarWorkshopServiceCommand>
{
    public DeleteCarWorkshopServiceCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .GreaterThan(0);
    }
}
