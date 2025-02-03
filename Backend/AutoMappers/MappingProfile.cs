using AutoMapper;
using Backend.DTOs.Beer;
using Backend.Models;

namespace Backend.AutoMappers;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<BeerInsertDto, Beer>();
        CreateMap<Beer, BeerDto>()
            .ForMember(dto => dto.Id,
                m => m.MapFrom(beer => beer.BeerID));
        
        CreateMap<BeerUpdateDto, Beer>();
    }
}