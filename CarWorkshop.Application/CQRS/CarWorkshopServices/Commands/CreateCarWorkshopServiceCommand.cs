using CarWorkshop.Application.Models;
using MediatR;

namespace CarWorkshop.Application.CQRS.CarWorkshopServices.Commands;

public record CreateCarWorkshopServiceCommand(string CarWorkshopEncodedName = default!)
    : CarWorkshopServiceDTO, IRequest;
