using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Base.Interfaces;
using PhoneBook.Web.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using PhoneBook.Database.Models;

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
        var query = Context.Contacts.Where(x => !x.SoftDelete);
        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(x =>x.LastName != null && x.LastName.Contains(search));
        var items = await query.OrderBy(x => x.LastName).ProjectTo<ContactGridItem>(Service.Mapper.ConfigurationProvider).ToListAsync();
        return new JsonResult(new { Result = "OK", Records = items });
    }

    public async Task<JsonResult> Delete(int id)
    {
        var contact = await Context.Contacts.FirstOrDefaultAsync(x => x.Id == id);
        if(contact != null)
        {
            Service.Context.Attach(contact);
            contact.SoftDelete = true;
            await Context.SaveChangesAsync();
        }
        return JsonOK();
    }

    [HttpPost]
    public async Task<JsonResult> Add([FromBody] ContactEditModel model)
    {
        var contact = Service.Mapper.Map<Contact>(model);
        await Context.Contacts.AddAsync(contact);
        await Context.SaveChangesAsync();
        return JsonOK();
    }

    [HttpPost]
    public async Task<JsonResult> Edit([FromBody] ContactEditModel model)
    {
        var contact = await Context.Contacts.FirstOrDefaultAsync(x => x.Id == model.Id);
        if (contact != null)
        {
            Context.Attach(contact);
            Service.Mapper.Map(model, contact);
            await Context.SaveChangesAsync();
        }
        return JsonOK();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}