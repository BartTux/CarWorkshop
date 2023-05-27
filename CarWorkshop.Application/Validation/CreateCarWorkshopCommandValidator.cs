using CarWorkshop.Application.CQRS.CarWorkshops.Commands;
using CarWorkshop.Domain.Contracts;
using FluentValidation;

namespace CarWorkshop.Application.Validation;

public class CreateCarWorkshopCommandValidator : AbstractValidator<CreateCarWorkshopCommand>
{
    public CreateCarWorkshopCommandValidator(ICarWorkshopRepository carWorkshopRepository)
    {
        var nameMinLength = 2;
        var nameMaxLength = 20;

        var phoneNumberMinLength = 8;
        var phoneNumberMaxLength = 12;

        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(nameMinLength)
                .WithMessage($"Name field should have at least { nameMinLength } characters")
            .MaximumLength(nameMaxLength)
                .WithMessage($"Name field should have at last { nameMaxLength } characters")
            .Custom((value, context) => 
            {
                var isCarWorkshopExists = 
                    carWorkshopRepository.GetByName(value).Result is not null;

                if (isCarWorkshopExists) 
                    context.AddFailure($"""Given name "{ value }" already exists""");
            });
            

        RuleFor(x => x.Description)
            .NotEmpty()
                .WithMessage("Please enter description");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .MinimumLength(phoneNumberMinLength)
            .MaximumLength(phoneNumberMaxLength);
    }
}
