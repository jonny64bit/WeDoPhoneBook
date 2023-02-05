using AutoMapper;
using Microsoft.Extensions.Logging;
using PhoneBook.Database;

namespace PhoneBook.Base.Interfaces;

public interface IService
{
    DAL Context { get; }
    ILogger Logger { get; }
    ILoggerFactory LoggerFactory { get; }
    IMapper Mapper { get; }
}