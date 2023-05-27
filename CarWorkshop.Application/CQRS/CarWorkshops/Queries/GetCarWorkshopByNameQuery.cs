using CarWorkshop.Application.Models;
using MediatR;

namespace CarWorkshop.Application.CQRS.CarWorkshops.Queries;

public record GetCarWorkshopByNameQuery(string EncodedName) : IRequest<CarWorkshopDTO>;
