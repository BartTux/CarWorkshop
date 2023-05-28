using CarWorkshop.Application.CQRS.CarWorkshopServices.Queries;
using CarWorkshop.Application.Models;
using CarWorkshop.Domain.Contracts;
using AutoMapper;
using MediatR;

namespace CarWorkshop.Application.CQRS.CarWorkshopServices.QueryHandlers;

public class GetCarWorkshopServiceQueryHandler 
    : IRequestHandler<GetCarWorkshopServiceQuery, CarWorkshopServiceDTO>
{
    private readonly IMapper _mapper;
    private readonly ICarWorkshopServiceRepository _repository;

    public GetCarWorkshopServiceQueryHandler(IMapper mapper,
                                             ICarWorkshopServiceRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<CarWorkshopServiceDTO> Handle(GetCarWorkshopServiceQuery request,
                                                    CancellationToken cancellationToken)
    {
        var carWorkshopService = await _repository.GetById(request.Id);
        var carWorkshopServiceDto = _mapper.Map<CarWorkshopServiceDTO>(carWorkshopService);

        return carWorkshopServiceDto;
    }
}
