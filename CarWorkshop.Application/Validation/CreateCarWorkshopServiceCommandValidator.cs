using CarWorkshop.Application.CQRS.CarWorkshopServices.Commands;
using CarWorkshop.Domain.Contracts;
using FluentValidation;

namespace CarWorkshop.Application.Validation;

public class CreateCarWorkshopServiceCommandValidator : AbstractValidator<CreateCarWorkshopServiceCommand>
{
    public CreateCarWorkshopServiceCommandValidator(ICarWorkshopServiceRepository repository)
    {
        RuleFor(x => x.Cost)
            .NotEmpty()
            .NotNull()
            .GreaterThan(0);

        RuleFor(x => x.Description)
            .NotEmpty()
            .NotNull()
            .Custom((value, context) =>
            {
                var isCarWorkshopServiceExist =
                    repository.GetByDescription(value).Result is not null;

                if (isCarWorkshopServiceExist)
                    context.AddFailure("Service with given name already exists");
            });

        RuleFor(x => x.CarWorkshopEncodedName)
            .NotEmpty()
            .NotNull();
    }
}
