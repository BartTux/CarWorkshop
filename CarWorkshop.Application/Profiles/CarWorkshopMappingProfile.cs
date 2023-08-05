using CarWorkshop.Domain.Entities;
using CarWorkshop.Application.Models;
using CarWorkshop.Application.Services.Contracts;
using CarWorkshop.Application.CQRS.CarWorkshops.Commands;
using CarWorkshop.Application.CQRS.CarWorkshopServices.Commands;
using AutoMapper;

namespace CarWorkshop.Application.Profiles;

internal class CarWorkshopMappingProfile : Profile
{
    public CarWorkshopMappingProfile(IUserContextService userContextService)
    {
        AllowNullCollections = true;
        var user = userContextService.GetCurrentUser();

        CreateMap<CarWorkshopDTO, Domain.Entities.CarWorkshop>()
            .ForMember(c => c.ContactDetails, opt => opt.MapFrom(src => new ContactDetails
            {
                PhoneNumber = src.PhoneNumber,
                City = src.City,
                Street = src.Street,
                PostalCode = src.PostalCode
            }));
            //.ReverseMap();

        CreateMap<Domain.Entities.CarWorkshop, CarWorkshopDTO>()
            .ForMember(dto => dto.PhoneNumber, opt => opt.MapFrom(src => src.ContactDetails.PhoneNumber))
            .ForMember(dto => dto.Street, opt => opt.MapFrom(src => src.ContactDetails.Street))
            .ForMember(dto => dto.City, opt => opt.MapFrom(src => src.ContactDetails.City))
            .ForMember(dto => dto.PostalCode, opt => opt.MapFrom(src => src.ContactDetails.PostalCode))
            .ForMember(dto => dto.IsEditable, opt => opt.MapFrom(src => 
                user != null && (user.Id == src.CreatedById && user.IsInRole("Owner"))));

        CreateMap<CarWorkshopDTO, EditCarWorkshopCommand>();

        CreateMap<EditCarWorkshopCommand, Domain.Entities.CarWorkshop>()
            .ForMember(c => c.ContactDetails, opt => opt.MapFrom(src => new ContactDetails
            {
                PhoneNumber = src.PhoneNumber,
                City = src.City,
                Street = src.Street,
                PostalCode = src.PostalCode
            }));

        CreateMap<CarWorkshopServiceDTO, CarWorkshopService>()
            .ReverseMap();

        CreateMap<CarWorkshopServiceDTO, EditCarWorkshopServiceCommand>();

        CreateMap<Cart, CartDTO>();
        CreateMap<CarWorkshopServiceCart, CartServiceDTO>()
            .ForMember(dto => dto.Description, opt => opt.MapFrom(src => src.CarWorkshopService.Description))
            .ForMember(dto => dto.Cost, opt => opt.MapFrom(src => src.CarWorkshopService.Cost))
            .ForMember(dto => dto.TotalCost, opt => opt.MapFrom(src => src.CarWorkshopService.Cost * src.Quantity));
    }
}
