using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Base.Interfaces;
using PhoneBook.Web.Models;

namespace PhoneBook.Web.Controllers;

public class HomeController : BaseController
{
    public HomeController(IService service) : base(service)
    {
    }

    public IActionResult Index()
    {
        //TODO remove...
        var count = this.Service.Context.Contacts.Count();

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}