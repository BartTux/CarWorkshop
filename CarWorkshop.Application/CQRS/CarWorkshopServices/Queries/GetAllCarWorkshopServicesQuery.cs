using CarWorkshop.Application.Models;
using MediatR;

namespace CarWorkshop.Application.CQRS.CarWorkshopServices.Queries;

public record GetAllCarWorkshopServicesQuery(string CarWorkshopEncodedName = default!)
    : QueryRequest, IRequest<QueryResponse<CarWorkshopServiceDTO>>;
