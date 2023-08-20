using AutoMapper;
using CarWorkshop.Application.CQRS.Carts.Queries;
using CarWorkshop.Application.Models;
using CarWorkshop.Application.Services.Contracts;
using CarWorkshop.Domain.Contracts;
using MediatR;

namespace CarWorkshop.Application.CQRS.Carts.QueryHandlers;

public class GetCartServiceQueryHandler : IRequestHandler<GetCartServiceQuery, CartServiceDTO?>
{
    private readonly IUserContextService _userContextService;
    private readonly ICartRepository _repository;
    private readonly IMapper _mapper;

    public GetCartServiceQueryHandler(IUserContextService userContextService,
                                      ICartRepository repository,
                                      IMapper mapper)
    {
        _userContextService = userContextService;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CartServiceDTO?> Handle(GetCartServiceQuery request,
                                              CancellationToken cancellationToken)
    {
        var userId = _userContextService.UserId;
        var cartService = await _repository.GetServiceById(userId, request.ServiceId);

        if (cartService is null) return null;

        var cartServiceDto = _mapper.Map<CartServiceDTO>(cartService);

        return cartServiceDto;
    }
}
