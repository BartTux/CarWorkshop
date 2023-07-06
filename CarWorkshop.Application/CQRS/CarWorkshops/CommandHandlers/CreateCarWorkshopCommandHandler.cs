using CarWorkshop.Domain.Contracts;
using AutoMapper;
using MediatR;
using CarWorkshop.Application.CQRS.CarWorkshops.Commands;
using CarWorkshop.Application.Services.Contracts;

namespace CarWorkshop.Application.CQRS.CarWorkshops.CommandHandlers;

public class CreateCarWorkshopCommandHandler : IRequestHandler<CreateCarWorkshopCommand>
{
    private readonly IMapper _mapper;
    private readonly IUserContextService _userContextService;
    private readonly ICarWorkshopRepository _carWorkshopRepository;

    public CreateCarWorkshopCommandHandler(IMapper mapper,
                                           IUserContextService userContextService,
                                           ICarWorkshopRepository carWorkshopRepository)
    {
        _mapper = mapper;
        _userContextService = userContextService;
        _carWorkshopRepository = carWorkshopRepository;
    }

    public async Task Handle(CreateCarWorkshopCommand request, CancellationToken cancellationToken)
    {
        var user = _userContextService!.GetCurrentUser();

        if (user is null || !user.IsInRole("Owner")) return;

        var carWorkshop = _mapper.Map<Domain.Entities.CarWorkshop>(request);

        carWorkshop.EncodeName();
        carWorkshop.CreatedById = user.Id;

        await _carWorkshopRepository.Create(carWorkshop);
    }
}
