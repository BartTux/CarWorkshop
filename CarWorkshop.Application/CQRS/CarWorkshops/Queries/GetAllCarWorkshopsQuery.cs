using CarWorkshop.Application.Models;
using MediatR;

namespace CarWorkshop.Application.CQRS.CarWorkshops.Queries;

public record GetAllCarWorkshopsQuery() : IRequest<IEnumerable<CarWorkshopDTO>>;
