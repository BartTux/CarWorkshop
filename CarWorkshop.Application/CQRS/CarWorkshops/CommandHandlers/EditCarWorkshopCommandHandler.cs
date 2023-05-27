using CarWorkshop.Domain.Contracts;
using MediatR;
using CarWorkshop.Application.Services;
using CarWorkshop.Application.CQRS.CarWorkshops.Commands;

namespace CarWorkshop.Application.CQRS.CarWorkshops.CommandHandlers;

public class EditCarWorkshopCommandHandler : IRequestHandler<EditCarWorkshopCommand>
{
    private readonly IUserContextService _userContextService;
    private readonly ICarWorkshopRepository _carWorkshopRepository;

    public EditCarWorkshopCommandHandler(IUserContextService userContextService,
                                         ICarWorkshopRepository carWorkshopRepository)
    {
        _userContextService = userContextService;
        _carWorkshopRepository = carWorkshopRepository;
    }

    public async Task Handle(EditCarWorkshopCommand request,
                             CancellationToken cancellationToken)
    {
        var carWorkshop = await _carWorkshopRepository.GetByEncodedName(request.EncodedName);
        var user = _userContextService.GetCurrentUser();

        if (user is null
            || carWorkshop.CreatedById != user.Id && !user.IsInRole("Owner")) return;

        carWorkshop.Description = request.Description;
        carWorkshop.About = request.About;
        carWorkshop.ContactDetails.PhoneNumber = request.PhoneNumber;
        carWorkshop.ContactDetails.City = request.City;
        carWorkshop.ContactDetails.Street = request.Street;
        carWorkshop.ContactDetails.PostalCode = request.PostalCode;

        await _carWorkshopRepository.Commit();
    }
}
