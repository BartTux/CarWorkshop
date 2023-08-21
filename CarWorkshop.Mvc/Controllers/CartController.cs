using CarWorkshop.Application.CQRS.Carts.Queries;
using CarWorkshop.Application.CQRS.Carts.Commands;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using CarWorkshop.Mvc.ViewModels;

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
    
    [HttpGet("{controller}/Service")]
    public async Task<IActionResult> GetServices()
    {
        var model = await _mediator.Send(new GetAllCartServicesQuery());
        var viewModel = new CartViewModel { CartServices = model };

        return PartialView("~/Views/Shared/_GetCartServices.cshtml", viewModel);
    }

    [HttpPost("{controller}/Service/{serviceId}/Add")]
    public async Task<IActionResult> AddService([FromRoute] int serviceId)
    {
        var cartService = await _mediator.Send(new GetCartServiceQuery(serviceId));
        var isCartServiceExist = cartService is not null;
            
        if (isCartServiceExist)
            return Json(new { url = Url.Action("IncreaseService", "Cart", new { serviceId }) });

        await _mediator.Send(new AddServiceToCartCommand(serviceId));
        return Ok();
    }

    [HttpPatch("{controller}/Service/{serviceId}/Increase")]
    public async Task<IActionResult> IncreaseService([FromRoute] int serviceId)
    {
        await _mediator.Send(new IncreaseCartServiceCommand(serviceId));
        return Ok();
    }

    [HttpPatch("{controller}/Service/{serviceId}/Decrease")]
    public async Task<IActionResult> DecreaseService([FromRoute] int serviceId)
    {
        await _mediator.Send(new DecreaseCartServiceCommand(serviceId));
        return Ok();
    }

    [HttpGet("{controller}/Service/{serviceId}/Delete")]
    public IActionResult GetDeleteService([FromRoute] int serviceId)
    {
        var model = new DeleteCartServiceCommand(serviceId);
        return PartialView("~/Views/Shared/_DeleteCartService.cshtml", model);
    }

    [HttpDelete("{controller}/Service/{serviceId}/Delete")]
    public async Task<IActionResult> DeleteService([FromRoute] int serviceId)
    {
        await _mediator.Send(new DeleteCartServiceCommand(serviceId));
        return Ok();
    }
}
