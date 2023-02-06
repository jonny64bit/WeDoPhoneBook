using System.Data.Common;
using PhoneBook.Database;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq.AutoMock;
using PhoneBook.Base.Interfaces;

namespace PhoneBook.Web.Tests
{
    public class BaseTest
    {
        protected readonly AutoMocker Mocker;

        private readonly DbConnection _connection;
        protected readonly DAL Context;
        private readonly ILoggerFactory _loggerFactory;

        protected BaseTest()
        {
            var contextOptions = new DbContextOptionsBuilder<DAL>()
                .UseSqlite(CreateInMemoryDatabase())
                .Options;

            _connection = RelationalOptionsExtension.Extract(contextOptions).Connection;

            var services = new ServiceCollection();

            services.AddDbContext<DAL>(o => o.UseSqlite(CreateInMemoryDatabase()));

            services.AddLogging();

            var provider = services.BuildServiceProvider();

            Context = provider.GetRequiredService<DAL>();
            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();

            _loggerFactory = provider.GetRequiredService<ILoggerFactory>();

            Mocker = new AutoMocker(Moq.MockBehavior.Default);
            Mocker.Use(Context);

            var serviceMoq = Mocker.GetMock<IService>().Setup(x => x.Context).Returns(Context);
            Mocker.Use(serviceMoq);
        }

        private static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");

            connection.Open();

            return connection;
        }

        public void Dispose()
        {
            Context.Dispose();
            _connection.Dispose();
        }
    }
}