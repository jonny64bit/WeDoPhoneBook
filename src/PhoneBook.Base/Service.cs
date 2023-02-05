using AutoMapper;
using Microsoft.Extensions.Logging;
using PhoneBook.Base.Interfaces;
using PhoneBook.Database;

namespace PhoneBook.Base;

public class Service : IService
{
    public Service(DAL context, ILoggerFactory loggerFactory, IMapper mapper)
    {
        Context = context;
        Logger = loggerFactory.CreateLogger(GetType().Name);
        LoggerFactory = loggerFactory;
        Mapper = mapper;
    }
    
    public DAL Context { get; }
    public ILogger Logger { get; }
    public ILoggerFactory LoggerFactory { get; }
    public IMapper Mapper { get; }
}