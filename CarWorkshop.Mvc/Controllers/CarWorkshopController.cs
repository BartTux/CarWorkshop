using CarWorkshop.Application.CQRS.CarWorkshops.Commands;
using CarWorkshop.Application.CQRS.CarWorkshops.Queries;
using CarWorkshop.Application.CQRS.CarWorkshopServices.Queries;
using CarWorkshop.Application.Filters;
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

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var carWorkshops = await _mediator.Send(new GetAllCarWorkshopsQuery());
        return View(carWorkshops);
    }

    [HttpGet]
    [Authorize(Roles = "Owner")]
    public IActionResult Create() => View();

    [HttpPost]
    [Authorize(Roles = "Owner")]
    [ViewValidation]
    public async Task<IActionResult> Create([FromForm] CreateCarWorkshopCommand command)
    {
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
    [ViewValidation]
    public async Task<IActionResult> Edit([FromRoute] string encodedName,
                                          [FromForm] EditCarWorkshopCommand command)
    {
        await _mediator.Send(command);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("{controller}/{encodedName}/CarWorkshopService")]
    public async Task<IActionResult> GetAllServices([FromRoute] string encodedName,
                                                    [FromQuery] string? searchPhrase,
                                                    [FromQuery] int pageNumber,
                                                    [FromQuery] int pageSize)
    {
        var query = new GetAllCarWorkshopServicesQuery(encodedName)
        {
            SearchPhrase = searchPhrase,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var carWorkshopServiceQueryResult = await _mediator.Send(query);

        var viewModel = new CarWorkshopServicesViewModel
        {
            EncodedName = encodedName,
            QueryResult = carWorkshopServiceQueryResult
        };

        return PartialView("~/Views/Shared/_GetAllCarWorkshopServices.cshtml", viewModel);
    }
}