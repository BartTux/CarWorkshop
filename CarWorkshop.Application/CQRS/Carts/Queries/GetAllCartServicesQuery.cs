﻿using CarWorkshop.Application.Models;
using MediatR;

namespace CarWorkshop.Application.CQRS.Carts.Queries;

public record GetAllCartServicesQuery() : IRequest<List<CartServiceDTO>>;