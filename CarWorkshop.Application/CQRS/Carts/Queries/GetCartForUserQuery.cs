using CarWorkshop.Application.Models;
using CarWorkshop.Domain.Entities;
using MediatR;

namespace CarWorkshop.Application.CQRS.Carts.Queries;

public record GetCartForUserQuery() : IRequest<CartDTO>;
