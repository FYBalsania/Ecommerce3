using AutoMapper;
using Ecommerce3.Application.DTOs.Brand;
using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Application.Mappings;

public class BrandProfile : Profile
{
    public BrandProfile()
    {
        CreateMap<Brand, BrandListItemDTO>()
            .ForMember(x => x.CreatedUserFullName, opt => opt.MapFrom(s => s.CreatedByUser.FullName));
        CreateMap<Brand, BrandDTO>()
            .ForMember(x => x.CreatedUserFullName, opt => opt.MapFrom(s => s.CreatedByUser.FullName))
            .ForMember(x => x.UpdatedUserFullName, opt => opt.MapFrom(s => s.UpdatedByUser.FullName))
            .ForMember(x => x.DeletedUserFullName, opt => opt.MapFrom(s => s.DeletedByUser.FullName));
    }
}