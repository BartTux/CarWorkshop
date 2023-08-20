using MediatR;

namespace CarWorkshop.Application.CQRS.Carts.Commands;

public record IncreaseCartServiceCommand(int CarWorkshopServiceId) : IRequest;
