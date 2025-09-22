using AutoMapper;
using Ecommerce3.Application.DTOs;
using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Application.Mappings;

public class BrandProfile : Profile
{
    public BrandProfile()
    {
        CreateMap<Brand, BrandListItemDTO>()
            .ForMember(x => x.CreatedUserFullName, opt => opt.MapFrom(s => s.CreatedByUser.FullName));
    }
}