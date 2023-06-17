using AutoMapper;
using CarWorkshop.Application.CQRS.CarWorkshopServices.Queries;
using CarWorkshop.Application.Models;
using CarWorkshop.Domain.Contracts;
using MediatR;

namespace CarWorkshop.Application.CQRS.CarWorkshopServices.QueryHandlers;

public class GetAllCarWorkshopServicesQueryHandler
    : IRequestHandler<GetAllCarWorkshopServicesQuery, QueryResponse<CarWorkshopServiceDTO>>
{
    private readonly IMapper _mapper;
    private readonly ICarWorkshopServiceRepository _repository;

    public GetAllCarWorkshopServicesQueryHandler(IMapper mapper,
                                                 ICarWorkshopServiceRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<QueryResponse<CarWorkshopServiceDTO>> Handle(GetAllCarWorkshopServicesQuery request,
                                                                   CancellationToken cancellationToken)
    {
        var queryResult = await _repository.GetByEncodedName(
            request.CarWorkshopEncodedName,
            request.SearchPhrase,
            request.PageNumber,
            request.PageSize
        );

        var carWorkshopServiceDtos = queryResult.Data
            .Select(c => new CarWorkshopServiceDTO
            {
                Id = c.Id,
                Description = c.Description,
                Cost = c.Cost
            })
            .ToList();

        var queryResultDto = new QueryResponse<CarWorkshopServiceDTO>
        {
            Data = carWorkshopServiceDtos,
            TotalCount = queryResult.TotalCount,
            SearchPhrase = request.SearchPhrase,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };

        //var carWorkshopServiceDtos = 
        //    _mapper.Map<IEnumerable<CarWorkshopServiceDTO>>(carWorkshopServices);

        return queryResultDto;
    }
}
