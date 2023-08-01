using CarWorkshop.Application.CQRS.Carts.Queries;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace CarWorkshop.Mvc.Controllers;

public class CartController : Controller
{
    private readonly IMediator _mediator;

    public CartController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var cartDto = await _mediator.Send(new GetCartForUserQuery());
        return View(cartDto);
    }
}
