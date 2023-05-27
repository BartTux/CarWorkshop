using CarWorkshop.Application.CQRS.CarWorkshopServices.Commands;
using CarWorkshop.Domain.Contracts;
using MediatR;

namespace CarWorkshop.Application.CQRS.CarWorkshopServices.CommandHandlers;

public class DeleteCarWorkshopServiceCommandHandler : IRequestHandler<DeleteCarWorkshopServiceCommand>
{
    private readonly ICarWorkshopServiceRepository _carWorkshopServiceRepository;

    public DeleteCarWorkshopServiceCommandHandler(ICarWorkshopServiceRepository carWorkshopServiceRepository)
    {
        _carWorkshopServiceRepository = carWorkshopServiceRepository;
    }

    public async Task Handle(DeleteCarWorkshopServiceCommand request,
                             CancellationToken cancellationToken)
        =>
        await _carWorkshopServiceRepository.Delete(request.Id);
}
