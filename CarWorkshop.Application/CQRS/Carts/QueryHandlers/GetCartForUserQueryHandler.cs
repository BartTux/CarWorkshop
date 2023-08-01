using CarWorkshop.Application.CQRS.Carts.Queries;
using CarWorkshop.Application.Models;
using CarWorkshop.Application.Services.Contracts;
using CarWorkshop.Domain.Contracts;
using MediatR;
using AutoMapper;

namespace CarWorkshop.Application.CQRS.Carts.QueryHandlers;

public class GetCartForUserQueryHandler : IRequestHandler<GetCartForUserQuery, CartDTO>
{
    private readonly IUserContextService _userContextService;
    private readonly ICartRepository _repository;
    private readonly IMapper _mapper;

    public GetCartForUserQueryHandler(IUserContextService userContextService,
                                      ICartRepository repository,
                                      IMapper mapper)
    {
        _userContextService = userContextService;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CartDTO> Handle(GetCartForUserQuery request, CancellationToken cancellationToken)
    {
        var userId = _userContextService.UserId;
        var cart = await _repository.GetCartWithServicesForUser(userId);
        
        var cartDto = _mapper.Map<CartDTO>(cart);
        
        cartDto.Services = cart.ServiceCarts
            .Select(_mapper.Map<CartServiceDTO>)
            .ToList();

        cartDto.Services
            .ForEach(s => cartDto.TotalSumCost += s.TotalCost);

        return cartDto;
    }
}
