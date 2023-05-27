using CarWorkshop.Domain.Contracts;
using MediatR;
using AutoMapper;
using CarWorkshop.Application.Models;
using CarWorkshop.Application.CQRS.CarWorkshops.Queries;

namespace CarWorkshop.Application.CQRS.CarWorkshops.QueryHandlers;

public class GetAllCarWorkshopsQueryHandler
    : IRequestHandler<GetAllCarWorkshopsQuery, IEnumerable<CarWorkshopDTO>>
{
    private readonly IMapper _mapper;
    private readonly ICarWorkshopRepository _carWorkshopRepository;

    public GetAllCarWorkshopsQueryHandler(IMapper mapper,
                                          ICarWorkshopRepository carWorkshopRepository)
    {
        _mapper = mapper;
        _carWorkshopRepository = carWorkshopRepository;
    }

    public async Task<IEnumerable<CarWorkshopDTO>> Handle(GetAllCarWorkshopsQuery request,
                                                          CancellationToken cancellationToken)
    {
        var carWorkshops = await _carWorkshopRepository.GetAll();
        var carWorkshopsDto = _mapper.Map<IEnumerable<CarWorkshopDTO>>(carWorkshops);

        return carWorkshopsDto;
    }
}
