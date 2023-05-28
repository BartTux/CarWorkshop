using CarWorkshop.Application.Models;
using MediatR;

namespace CarWorkshop.Application.CQRS.CarWorkshopServices.Queries;

public record GetCarWorkshopServiceQuery(int Id) : IRequest<CarWorkshopServiceDTO>;
