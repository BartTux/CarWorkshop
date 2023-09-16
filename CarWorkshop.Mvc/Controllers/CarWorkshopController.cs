using CarWorkshop.Application.CQRS.CarWorkshops.Commands;
using CarWorkshop.Application.CQRS.CarWorkshops.Queries;
using CarWorkshop.Application.CQRS.CarWorkshopServices.Queries;
using CarWorkshop.Application.Filters;
using CarWorkshop.Mvc.Models;
using CarWorkshop.Mvc.ViewModels;
using CarWorkshop.Mvc.Extensions;
using CarWorkshop.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using MediatR;

namespace CarWorkshop.Mvc.Controllers;

[Authorize(Roles = "Owner")]
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
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var carWorkshops = await _mediator.Send(new GetAllCarWorkshopsQuery());
        var viewModel = new CarWorkshopViewModel { CarWorkshops = (List<CarWorkshopDTO>)carWorkshops };

        return View(viewModel);
    }

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    [ViewValidation]
    public async Task<IActionResult> Create([FromForm] CreateCarWorkshopCommand command)
    {
        await _mediator.Send(command);
        this.SetNotification(NotificationTypeEnum.success, $"Created carworkshop { command.Name }");

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("{controller}/{encodedName}/Details")]
    [AllowAnonymous]
    public async Task<IActionResult> Details([FromRoute] string encodedName)
    {
        var carWorkshop = await _mediator.Send(new GetCarWorkshopByNameQuery(encodedName));
        return View(carWorkshop);
    }

    [HttpGet("{controller}/{encodedName}/Edit")]
    public async Task<IActionResult> Edit([FromRoute] string encodedName)
    {
        var carWorkshop = await _mediator.Send(new GetCarWorkshopByNameQuery(encodedName));

        if (!carWorkshop.IsEditable) 
            return RedirectToAction("NoAccess", "Home");
        
        var command = _mapper.Map<EditCarWorkshopCommand>(carWorkshop);
        return View(command);
    }

    [HttpPost("{controller}/{encodedName}/Edit")]
    [ViewValidation]
    public async Task<IActionResult> Edit([FromRoute] string encodedName,
                                          [FromForm] EditCarWorkshopCommand command)
    {
        await _mediator.Send(command);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("{controller}/{encodedName}/CarWorkshopService")]
    [AllowAnonymous]
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