using AutoMapper;
using CarWorkshop.Application.CQRS.CarWorkshops.Queries;
using CarWorkshop.Application.Models;
using CarWorkshop.Domain.Contracts;
using MediatR;

namespace CarWorkshop.Application.CQRS.CarWorkshops.QueryHandlers;

public class GetCarWorkshopByNameQueryHandler
    : IRequestHandler<GetCarWorkshopByNameQuery, CarWorkshopDTO>
{
    private readonly IMapper _mapper;
    private readonly ICarWorkshopRepository _carWorkshopRepository;

    public GetCarWorkshopByNameQueryHandler(IMapper mapper,
                                            ICarWorkshopRepository carWorkshopRepository)
    {
        _mapper = mapper;
        _carWorkshopRepository = carWorkshopRepository;
    }

    public async Task<CarWorkshopDTO> Handle(GetCarWorkshopByNameQuery request,
                                             CancellationToken cancellationToken)
    {
        var carWorkshop = await _carWorkshopRepository.GetByEncodedName(request.EncodedName);
        var carWorkshopDto = _mapper.Map<CarWorkshopDTO>(carWorkshop);

        return carWorkshopDto;
    }
}
