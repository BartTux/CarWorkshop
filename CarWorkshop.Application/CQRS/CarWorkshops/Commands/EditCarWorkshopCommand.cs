using CarWorkshop.Application.Models;
using MediatR;

namespace CarWorkshop.Application.CQRS.CarWorkshops.Commands;

public record EditCarWorkshopCommand() : CarWorkshopDTO, IRequest;
