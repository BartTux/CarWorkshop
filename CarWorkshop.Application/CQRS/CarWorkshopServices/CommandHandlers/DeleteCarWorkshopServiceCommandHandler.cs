using CarWorkshop.Application.Authorization.Requirements;
using CarWorkshop.Application.CQRS.CarWorkshopServices.Commands;
using CarWorkshop.Application.Models;
using CarWorkshop.Application.Services.Contracts;
using CarWorkshop.Domain.Contracts;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarWorkshop.Application.CQRS.CarWorkshopServices.CommandHandlers;

public class DeleteCarWorkshopServiceCommandHandler : IRequestHandler<DeleteCarWorkshopServiceCommand>
{
    private readonly IUserContextService _userContextService;
    private readonly IAuthorizationService _authorizationService;
    private readonly ICarWorkshopServiceRepository _repository;
    private readonly ILogger<DeleteCarWorkshopServiceCommandHandler> _logger;

    public DeleteCarWorkshopServiceCommandHandler(IUserContextService userContextService,
                                                  IAuthorizationService authorizationService,
                                                  ICarWorkshopServiceRepository repository,
                                                  ILogger<DeleteCarWorkshopServiceCommandHandler> logger)
    {
        _userContextService = userContextService;
        _authorizationService = authorizationService;
        _repository = repository;
        _logger = logger;
    }

    public async Task Handle(DeleteCarWorkshopServiceCommand request,
                             CancellationToken cancellationToken)
    {
        var loggerSummary = $"CarWorkshopService with id: { request.Id }";
        _logger.LogInformation($"{ loggerSummary } DELETE action invoked.");

        var carWorkshopService = await _repository.GetById(request.Id);

        var authorizationResult = await _authorizationService.AuthorizeAsync(
            _userContextService.User,
            carWorkshopService,
            new ResourceOperationRequirement(ResourceOperation.Delete)
        );

        if (!authorizationResult.Succeeded) 
        {
            _logger.LogError($"{ loggerSummary } Unable to perform DELETE action");
            throw new Exception();
        }
        
        await _repository.Delete(request.Id);

        _logger.LogInformation($"{ loggerSummary } DELETE action performed successfully");
    }
}
