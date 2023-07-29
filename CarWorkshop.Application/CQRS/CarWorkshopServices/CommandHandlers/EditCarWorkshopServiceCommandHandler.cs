using CarWorkshop.Application.Authorization.Requirements;
using CarWorkshop.Application.CQRS.CarWorkshopServices.Commands;
using CarWorkshop.Application.Models;
using CarWorkshop.Application.Services.Contracts;
using CarWorkshop.Domain.Contracts;
using Microsoft.AspNetCore.Authorization;
using MediatR;

namespace CarWorkshop.Application.CQRS.CarWorkshopServices.CommandHandlers;

public class EditCarWorkshopServiceCommandHandler : IRequestHandler<EditCarWorkshopServiceCommand>
{
    private readonly IUserContextService _userContextService;
    private readonly IAuthorizationService _authorizationService;
    private readonly ICarWorkshopServiceRepository _repository;

    public EditCarWorkshopServiceCommandHandler(IUserContextService userContextService,
                                                IAuthorizationService authorizationService,
                                                ICarWorkshopServiceRepository repository)
    {
        _userContextService = userContextService;
        _authorizationService = authorizationService;
        _repository = repository;
    }

    public async Task Handle(EditCarWorkshopServiceCommand request,
                             CancellationToken cancellationToken)
    {
        var carWorkshopService = await _repository.GetById(request.Id);

        var authorizationResult = await _authorizationService.AuthorizeAsync(
            _userContextService.User,
            carWorkshopService.CarWorkshop,
            new ResourceOperationRequirement(ResourceOperation.Update));

        if (!authorizationResult.Succeeded)
            throw new Exception();

        carWorkshopService.Cost = request.Cost;
        carWorkshopService.Description = request.Description;

        await _repository.Commit();
    }
}
