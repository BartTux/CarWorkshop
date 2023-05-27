using MediatR;

namespace CarWorkshop.Application.CQRS.CarWorkshopServices.Commands;

public record DeleteCarWorkshopServiceCommand(int Id) : IRequest;
