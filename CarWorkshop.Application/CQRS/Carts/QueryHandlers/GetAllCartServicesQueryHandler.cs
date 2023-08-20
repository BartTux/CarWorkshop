using CarWorkshop.Application.CQRS.Carts.Queries;
using CarWorkshop.Application.Models;
using CarWorkshop.Application.Services.Contracts;
using CarWorkshop.Domain.Contracts;
using MediatR;
using AutoMapper;

namespace CarWorkshop.Application.CQRS.Carts.QueryHandlers;

public class GetAllCartServicesQueryHandler : IRequestHandler<GetAllCartServicesQuery, List<CartServiceDTO>>
{
    private readonly IUserContextService _userContextService;
    private readonly ICartRepository _repository;
    private readonly IMapper _mapper;

    public GetAllCartServicesQueryHandler(IUserContextService userContextService,
                                      ICartRepository repository,
                                      IMapper mapper)
    {
        _userContextService = userContextService;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<CartServiceDTO>> Handle(GetAllCartServicesQuery request,
                                                   CancellationToken cancellationToken)
    {
        var userId = _userContextService.UserId;
        var cartServices = await _repository.GetAllServices(userId);

        var cartServiceDtos = _mapper.Map<List<CartServiceDTO>>(cartServices);

        return cartServiceDtos;
    }
}
