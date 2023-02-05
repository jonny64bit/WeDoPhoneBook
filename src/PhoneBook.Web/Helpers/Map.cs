using AutoMapper;
using PhoneBook.Database.Models;
using PhoneBook.Web.Models;

namespace PhoneBook.Web.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Contact, ContactGridItem>()
                .ForMember(x=>x.Name, x=>x.MapFrom(c=>c.FirstName + " " + c.LastName));
        }
    }
}
