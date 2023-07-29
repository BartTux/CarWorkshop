using CarWorkshop.Application.CQRS.CarWorkshops.Commands;
using CarWorkshop.Domain.Contracts;
using FluentValidation;

namespace CarWorkshop.Application.Validation;

public class CreateCarWorkshopCommandValidator : AbstractValidator<CreateCarWorkshopCommand>
{
    private const int NAME_MIN_LENGTH = 2;
    private const int NAME_MAX_LENGTH = 20;

    private const int PHONE_NUMBER_MIN_LENGTH = 8;
    private const int PHONE_NUMBER_MAX_LENGTH = 12;

    public CreateCarWorkshopCommandValidator(ICarWorkshopRepository repository)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(NAME_MIN_LENGTH)
                .WithMessage($"Name field should have at least { NAME_MIN_LENGTH } characters")
            .MaximumLength(NAME_MAX_LENGTH)
                .WithMessage($"Name field should have at last { NAME_MAX_LENGTH } characters")
            .Custom((value, context) => 
            {
                var isCarWorkshopExists = 
                    repository.GetByName(value).Result is not null;

                if (isCarWorkshopExists) 
                    context.AddFailure($"""Given name "{ value }" already exists""");
            });
            

        RuleFor(x => x.Description)
            .NotEmpty()
                .WithMessage("Please enter description");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .MinimumLength(PHONE_NUMBER_MIN_LENGTH)
            .MaximumLength(PHONE_NUMBER_MAX_LENGTH);
    }
}
