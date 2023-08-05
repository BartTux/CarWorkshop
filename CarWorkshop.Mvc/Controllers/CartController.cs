using CarWorkshop.Application.CQRS.Carts.Queries;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using CarWorkshop.Application.CQRS.Carts.Commands;

namespace CarWorkshop.Mvc.Controllers;

public class CartController : Controller
{
    private readonly IMediator _mediator;

    public CartController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public IActionResult Index() => View();
    
    [HttpGet("/Cart/Services")]
    public async Task<IActionResult> GetServices()
    {
        var cartDto = await _mediator.Send(new GetCartForUserQuery());
        return PartialView("~/Views/Shared/_GetCartForUser.cshtml", cartDto);
    }

    [HttpPatch("/Cart/{cartId}/Services/{carWorkshopServiceId}/Increase")]
    public async Task<IActionResult> InreaseService([FromRoute] int cartId,
                                                    [FromRoute] int carWorkshopServiceId)
    {
        await _mediator.Send(new IncreaseCartServiceCommand(cartId, carWorkshopServiceId));
        return Ok();
    }
}
