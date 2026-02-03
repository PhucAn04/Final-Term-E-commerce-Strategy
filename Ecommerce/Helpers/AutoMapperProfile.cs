using AutoMapper;
using Ecommerce.ViewModels;

namespace Ecommerce.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterViewModel, ApplicationUser>();
        }
    }
}
