using CarWorkshop.Application.CQRS.CarWorkshops.Commands;
using FluentValidation;

namespace CarWorkshop.Application.Validation;

public class EditCarWorkshopByNameCommandValidator : AbstractValidator<EditCarWorkshopCommand>
{
    public EditCarWorkshopByNameCommandValidator()
    {
        var phoneNumberMinLength = 8;
        var phoneNumberMaxLength = 12;

        RuleFor(x => x.Description)
            .NotEmpty()
                .WithMessage("Please enter the description");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .MinimumLength(phoneNumberMinLength)
                .WithMessage($"Phone number should have at least { phoneNumberMinLength } characters")
            .MaximumLength(phoneNumberMaxLength)
                .WithMessage($"Phone number should have at last { phoneNumberMaxLength } characters");
    }
}
