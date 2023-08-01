using AutoMapper;
using CarWorkshop.Application.CQRS.CarWorkshopServices.Queries;
using CarWorkshop.Application.Models;
using CarWorkshop.Application.Services.Contracts;
using CarWorkshop.Domain.Contracts;
using MediatR;

namespace CarWorkshop.Application.CQRS.CarWorkshopServices.QueryHandlers;

public class GetAllCarWorkshopServicesQueryHandler
    : IRequestHandler<GetAllCarWorkshopServicesQuery, QueryResponse<CarWorkshopServiceDTO>>
{
    private readonly IMapper _mapper;
    private readonly IUserContextService _userContextService;
    private readonly ICarWorkshopRepository _carWorkshopRepository;
    private readonly ICarWorkshopServiceRepository _carWorkshopServiceRepository;

    public GetAllCarWorkshopServicesQueryHandler(IMapper mapper,
                                                 IUserContextService userContextService,
                                                 ICarWorkshopRepository carWorkshopRepository,
                                                 ICarWorkshopServiceRepository carWorkshopServiceRepository)
    {
        _mapper = mapper;
        _userContextService = userContextService;
        _carWorkshopRepository = carWorkshopRepository;
        _carWorkshopServiceRepository = carWorkshopServiceRepository;
    }

    public async Task<QueryResponse<CarWorkshopServiceDTO>> Handle(GetAllCarWorkshopServicesQuery request,
                                                                   CancellationToken cancellationToken)
    {
        var carWorkshop = await _carWorkshopRepository
            .GetByEncodedName(request.CarWorkshopEncodedName);

        var queryResult = await _carWorkshopServiceRepository
            .GetByEncodedName(
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
                Cost = c.Cost,
            })
            .ToList();

        var queryResultDto = new QueryResponse<CarWorkshopServiceDTO>
        {
            Data = carWorkshopServiceDtos,
            TotalCount = queryResult.TotalCount,
            SearchPhrase = request.SearchPhrase,
            PageNumber = queryResult.PageNumber,
            PageSize = request.PageSize,
            IsEditable = _userContextService.UserId == carWorkshop.CreatedById
        };

        //var carWorkshopServiceDtos = 
        //    _mapper.Map<IEnumerable<CarWorkshopServiceDTO>>(carWorkshopServices);

        return queryResultDto;
    }
}
