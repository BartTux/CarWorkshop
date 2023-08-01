using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CarWorkshop.Mvc.Models;

namespace CarWorkshop.Mvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        var persons = new List<Person>
        {
            new() { FirstName = "Bartek", LastName = "Nalepa" },
            new() { FirstName = "Kamil", LastName = "Konieczny" },
            new() { FirstName = "Mateusz", LastName = "Michałowicz" },
        };

        return View(persons);
    }

    public IActionResult About()
    {
        var posts = new List<Post>
        {
            new() { Title = "Example title", Description = "Example Desc", Tags = new[] { "JP2GMD", "papiez", "janczesko" } },
            new() { Title = "Example title2", Description = "Example Desc2", Tags = new[] { "bartek", "hot-crash", "nalepa" } },
            new() { Title = "Example title3", Description = "Example Desc3", Tags = new[] { "dupa", "cycki", "sex" } },
            new() { Title = "Example title4", Description = "Example Desc4", Tags = new[] { "mechanika", "stale", "rzepa" } },
            new() { Title = "Example title5", Description = "Example Desc5", Tags = new[] { "pies", "terier", "ofermka" } }
        };

        return View(posts);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult NoAccess() => View();
}
