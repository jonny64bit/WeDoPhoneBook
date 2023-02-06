using AutoMapper;
using PhoneBook.Database.Models;
using PhoneBook.Web.Models;

namespace PhoneBook.Web.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Contact, ContactGridItem>();
            CreateMap<ContactEditModel, Contact>();
        }
    }
}
