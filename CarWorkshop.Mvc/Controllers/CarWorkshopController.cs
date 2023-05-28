using CarWorkshop.Application.CQRS.CarWorkshops.Commands;
using CarWorkshop.Application.CQRS.CarWorkshopServices.Commands;
using CarWorkshop.Application.CQRS.CarWorkshops.Queries;
using CarWorkshop.Application.CQRS.CarWorkshopServices.Queries;
using CarWorkshop.Mvc.Models;
using CarWorkshop.Mvc.ViewModels;
using CarWorkshop.Mvc.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using MediatR;

namespace CarWorkshop.Mvc.Controllers;

public class CarWorkshopController : Controller
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CarWorkshopController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        var carWorkshops = await _mediator.Send(new GetAllCarWorkshopsQuery());
        return View(carWorkshops);
    }

    [Authorize(Roles = "Owner")]
    public IActionResult Create() => View();

    [HttpPost]
    [Authorize(Roles = "Owner")]
    public async Task<IActionResult> Create([FromForm] CreateCarWorkshopCommand command)
    {
        if (!ModelState.IsValid) return View(command);

        await _mediator.Send(command);

        this.SetNotification(NotificationTypeEnum.success, $"Created carworkshop { command.Name }");
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("{controller}/{encodedName}/Details")]
    public async Task<IActionResult> Details([FromRoute] string encodedName)
    {
        var carWorkshop = await _mediator.Send(new GetCarWorkshopByNameQuery(encodedName));
        return View(carWorkshop);
    }

    [HttpGet("{controller}/{encodedName}/Edit")]
    [Authorize(Roles = "Owner")]
    public async Task<IActionResult> Edit([FromRoute] string encodedName)
    {
        var carWorkshop = await _mediator.Send(new GetCarWorkshopByNameQuery(encodedName));

        if (!carWorkshop.IsEditable) 
            return RedirectToAction("NoAccess", "Home");

        var command = _mapper.Map<EditCarWorkshopCommand>(carWorkshop);
        return View(command);
    }

    [HttpPost("{controller}/{encodedName}/Edit")]
    [Authorize(Roles = "Owner")]
    public async Task<IActionResult> Edit([FromRoute] string encodedName,
                                          [FromForm] EditCarWorkshopCommand command)
    {
        if (!ModelState.IsValid) return View(command);

        await _mediator.Send(command);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("{controller}/CarWorkshopService")]
    [Authorize(Roles = "Owner")]
    public async Task<IActionResult> CreateService([FromForm] CreateCarWorkshopServiceCommand command)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        await _mediator.Send(command);
        return Ok();
    }

    [HttpGet("{controller}/{encodedName}/CarWorkshopService")]
    public async Task<IActionResult> GetAllServices([FromRoute] string encodedName,
                                                    [FromQuery] int pageNumber,
                                                    [FromQuery] int pageSize)
    {
        var query = new GetAllCarWorkshopServicesQuery(encodedName) 
        { 
            PageNumber = pageNumber, 
            PageSize = pageSize 
        };

        var carWorkshopServiceQueryResult = await _mediator.Send(query);

        var viewModel = new CarWorkshopServicesViewModel
        {
            EncodedName = encodedName,
            QueryResult = carWorkshopServiceQueryResult
        };

        return PartialView("_GetAllCarWorkshopServices", viewModel);
    }

    [HttpGet("{controller}/CarWorkshopService/Edit/{serviceId}")]
    [Authorize(Roles = "Owner")]
    public async Task<IActionResult> EditService([FromRoute] int serviceId)
    {
        var carWorkshopServiceDto = await _mediator.Send(new GetCarWorkshopServiceQuery(serviceId));
        var command = _mapper.Map<EditCarWorkshopServiceCommand>(carWorkshopServiceDto);

        return PartialView("_EditCarWorkshopService", command);
    }

    [HttpPost("{controller}/CarWorkshopService/Edit/{serviceId}")]
    [Authorize(Roles = "Owner")]
    public async Task<IActionResult> EditService([FromRoute] int serviceId,
                                                 [FromForm] EditCarWorkshopServiceCommand command)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        await _mediator.Send(command);
        return Ok();
    }

    [HttpGet("{controller}/CarWorkshopService/Delete/{serviceId}")]
    [Authorize(Roles = "Owner")]
    public IActionResult DeleteService([FromRoute] int serviceId)
    {
        var command = new DeleteCarWorkshopServiceCommand(serviceId);
        return PartialView("_DeleteCarWorkshopService", command);
    }

    [HttpPost("{controller}/CarWorkshopService/Delete/{serviceId}")]
    [Authorize(Roles = "Owner")]
    public async Task<IActionResult> DeleteService([FromRoute] int serviceId,
                                                   [FromForm] DeleteCarWorkshopServiceCommand command)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        await _mediator.Send(command);
        return Ok();
    }
}