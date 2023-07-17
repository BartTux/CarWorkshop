using CarWorkshop.Application.CQRS.CarWorkshopServices.Commands;
using CarWorkshop.Application.CQRS.CarWorkshopServices.Queries;
using CarWorkshop.Application.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using MediatR;

namespace CarWorkshop.Mvc.Controllers;

[Authorize(Roles = "Owner")]
public class CarWorkshopServiceController : Controller
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CarWorkshopServiceController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost("{controller}/Create")]
    [BadRequestValidation]
    public async Task<IActionResult> Create([FromForm] CreateCarWorkshopServiceCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [HttpGet("{controller}/Edit/{serviceId}")]
    public async Task<IActionResult> Edit([FromRoute] int serviceId)
    {
        var carWorkshopServiceDto = await _mediator
            .Send(new GetCarWorkshopServiceQuery(serviceId));

        var command = _mapper.Map<EditCarWorkshopServiceCommand>(carWorkshopServiceDto);

        return PartialView("~/Views/Shared/_EditCarWorkshopService.cshtml", command);
    }

    [HttpPost("{controller}/Edit/{serviceId}")]
    [BadRequestValidation]
    public async Task<IActionResult> Edit([FromRoute] int serviceId,
                                          [FromForm] EditCarWorkshopServiceCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [HttpGet("{controller}/Delete/{serviceId}")]
    public IActionResult Delete([FromRoute] int serviceId)
    {
        var command = new DeleteCarWorkshopServiceCommand(serviceId);
        return PartialView("~/Views/Shared/_DeleteCarWorkshopService.cshtml", command);
    }

    [HttpPost("{controller}/Delete/{serviceId}")]
    [BadRequestValidation]
    public async Task<IActionResult> Delete([FromRoute] int serviceId,
                                            [FromForm] DeleteCarWorkshopServiceCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
}
