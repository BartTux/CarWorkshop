using CarWorkshop.Application.CQRS.CarWorkshopServices.Commands;
using CarWorkshop.Domain.Contracts;
using MediatR;

namespace CarWorkshop.Application.CQRS.CarWorkshopServices.CommandHandlers;

public class DeleteCarWorkshopServiceCommandHandler : IRequestHandler<DeleteCarWorkshopServiceCommand>
{
    private readonly ICarWorkshopServiceRepository _repository;

    public DeleteCarWorkshopServiceCommandHandler(ICarWorkshopServiceRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteCarWorkshopServiceCommand request,
                             CancellationToken cancellationToken)
        => 
        await _repository.Delete(request.Id);
}
