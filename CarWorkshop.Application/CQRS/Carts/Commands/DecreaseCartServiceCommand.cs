using MediatR;

namespace CarWorkshop.Application.CQRS.Carts.Commands;

public record DecreaseCartServiceCommand(int CarWorkshopServiceId) : IRequest;
