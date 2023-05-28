using CarWorkshop.Application.Models;
using MediatR;

namespace CarWorkshop.Application.CQRS.CarWorkshopServices.Commands;

public record EditCarWorkshopServiceCommand : CarWorkshopServiceDTO, IRequest;
