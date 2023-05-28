using CarWorkshop.Application.CQRS.CarWorkshopServices.Commands;
using CarWorkshop.Application.Services;
using CarWorkshop.Domain.Contracts;
using MediatR;

namespace CarWorkshop.Application.CQRS.CarWorkshopServices.CommandHandlers;

public class EditCarWorkshopServiceCommandHandler : IRequestHandler<EditCarWorkshopServiceCommand>
{
    private readonly IUserContextService _userContext;
    private readonly ICarWorkshopServiceRepository _repository;

    public EditCarWorkshopServiceCommandHandler(IUserContextService userContext,
                                                ICarWorkshopServiceRepository repository)
    {
        _userContext = userContext;
        _repository = repository;
    }

    public async Task Handle(EditCarWorkshopServiceCommand request,
                             CancellationToken cancellationToken)
    {
        var user = _userContext.GetCurrentUser();
        var carWorkshopService = await _repository.GetById(request.Id);

        if (user is null || !user.IsInRole("Owner")) return;

        carWorkshopService.Cost = request.Cost;
        carWorkshopService.Description = request.Description;

        await _repository.Commit();
    }
}
