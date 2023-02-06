using Microsoft.AspNetCore.Mvc;
using PhoneBook.Base.Interfaces;
using PhoneBook.Database;
using PhoneBook.Web.Models;

namespace PhoneBook.Web.Controllers;

public class BaseController : Controller
{
    public DAL Context => Service.Context;
    public readonly IService Service;

    public BaseController(IService service)
    {
        Service = service;
    }

    protected JsonResult JsonOK() => Json(new GeneralJsonMessage<string> { Result = "OK", Detail = "" });
    protected JsonResult JsonErrorMessage(string message) => Json(new GeneralJsonMessage<string> {Result = "FAIL", Detail = message});
}