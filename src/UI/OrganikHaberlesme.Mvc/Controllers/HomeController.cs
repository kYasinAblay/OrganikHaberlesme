using System.Diagnostics;

using OrganikHaberlesme.Mvc.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper.Internal;

namespace OrganikHaberlesme.Mvc.Controllers;

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
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult ErrorPartial()
    {
        ViewBag.Error = HttpContext.Items.GetOrDefault("Error");
        return PartialView("_Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult NotAuthorized()
    {
        return View();
    }
}

