using PhoneBook.Web.Controllers;
using PhoneBook.Web.Models;
using Shouldly;

namespace PhoneBook.Web.Tests.Controllers
{
    /// <summary>
    /// This just provides an example.
    /// If this was for a production scenario i would aim for 100% coverage.
    /// </summary>
    public class HomeControllerTests : BaseTest
    {
        [Fact]
        public async Task Delete_Basic()
        {
            // arrange
            var controller = Mocker.CreateInstance<HomeController>();

            await Context.Contacts.AddAsync(new() { Id = 5 });
            await Context.SaveChangesAsync();

            // act
            var result = await controller.Delete(5);

            // assert
            result.Value.ShouldBeAssignableTo<GeneralJsonMessage<string>>().ShouldSatisfyAllConditions(
                x => x.Result.ShouldBe("OK"),
                x => x.Detail.ShouldBe(""));
        }
    }
}
