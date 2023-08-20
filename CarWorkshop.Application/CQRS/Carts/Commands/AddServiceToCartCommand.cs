using MediatR;

namespace CarWorkshop.Application.CQRS.Carts.Commands;

public record AddServiceToCartCommand(int CarWorkshopServiceId) : IRequest;
