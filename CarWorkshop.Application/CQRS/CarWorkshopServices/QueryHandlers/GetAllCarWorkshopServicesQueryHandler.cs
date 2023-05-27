using AutoMapper;
using CarWorkshop.Application.CQRS.CarWorkshopServices.Queries;
using CarWorkshop.Application.Models;
using CarWorkshop.Domain.Contracts;
using MediatR;

namespace CarWorkshop.Application.CQRS.CarWorkshopServices.QueryHandlers;

public class GetAllCarWorkshopServicesQueryHandler
    : IRequestHandler<GetAllCarWorkshopServicesQuery, QueryResultDTO<CarWorkshopServiceDTO>>
{
    private readonly IMapper _mapper;
    private readonly ICarWorkshopServiceRepository _carWorkshopServiceRepository;

    public GetAllCarWorkshopServicesQueryHandler(IMapper mapper,
                                                 ICarWorkshopServiceRepository carWorkshopServiceRepository)
    {
        _mapper = mapper;
        _carWorkshopServiceRepository = carWorkshopServiceRepository;
    }

    public async Task<QueryResultDTO<CarWorkshopServiceDTO>> Handle(GetAllCarWorkshopServicesQuery request,
                                                                 CancellationToken cancellationToken)
    {
        var queryResult = await _carWorkshopServiceRepository
            .GetByEncodedName(
                request.CarWorkshopEncodedName,
                request.PageNumber,
                request.PageSize
            );

        var carWorkshopServiceDtos = queryResult.Data.Select(c => new CarWorkshopServiceDTO
        {
            Id = c.Id,
            Description = c.Description,
            Cost = c.Cost
        }).ToList();

        var queryResultDto = new QueryResultDTO<CarWorkshopServiceDTO>
        {
            Data = carWorkshopServiceDtos,
            TotalCount = queryResult.TotalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };

        //var carWorkshopServiceDtos = 
        //    _mapper.Map<IEnumerable<CarWorkshopServiceDTO>>(carWorkshopServices);

        return queryResultDto;
    }
}
