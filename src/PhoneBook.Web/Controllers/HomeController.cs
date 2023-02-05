using System.Diagnostics;
using AutoMapper;
using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Base.Interfaces;
using PhoneBook.Web.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;

namespace PhoneBook.Web.Controllers;

public class HomeController : BaseController
{
    public HomeController(IService service) : base(service)
    {
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<JsonResult> Read(string? search)
    {
        var query = Service.Context.Contacts.Where(x => !x.SoftDelete);
        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(x =>x.LastName != null && x.LastName.Contains(search));
        var items = await query.OrderBy(x => x.LastName).ProjectTo<ContactGridItem>(Service.Mapper.ConfigurationProvider).ToListAsync();
        return new JsonResult(new { Result = "OK", Records = items });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}