using AutoMapper;
using Water_consumption_monitors.Dto;
using Water_consumption_monitors.DTO;
using Water_consumption_monitors.Models;

namespace Water_consumption_monitors.Helpers
{
    public class MappingProfile : Profile 
    {
        public MappingProfile()
        {
            CreateMap<Invoice, InvoiceDto>().ReverseMap();
            CreateMap<Slidedistribution, SlidedistributionDto>().ReverseMap();
            CreateMap<Subscriber, SubscriberDto>().ReverseMap();    
            CreateMap<Subscription , SubscriptionDto>().ReverseMap();
            CreateMap<TypesOfRealEstate , TypesOfRealEstateDto>().ReverseMap();
            CreateMap<AddRole, AddRoleDto>().ReverseMap();
            CreateMap<ApplicationUser, ApplicationUserDto>().ReverseMap();
            CreateMap<Auth, AuthDto>().ReverseMap();
            CreateMap<Register, RegisterDto>().ReverseMap();

        }
    }
}
