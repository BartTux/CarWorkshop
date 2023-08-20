using MediatR;

namespace CarWorkshop.Application.CQRS.Carts.Commands;

public record DeleteCartServiceCommand(int CarWorkshopServiceId) : IRequest;
