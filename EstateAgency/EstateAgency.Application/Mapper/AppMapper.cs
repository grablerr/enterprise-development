using EstateAgency.Application.Dtos;
using AutoMapper;
using EstateAgency.Domain.Entities;

namespace EstateAgency.Application.Mapper;

/// <summary>
/// AutoMapper profile for mapping between domain entities and their DTOs.
/// </summary>
public class AppMapper : Profile
{
    /// <summary>
    /// Initializes the mappings between DTOs and domain entities.
    /// </summary>
    public AppMapper()
    {
        CreateMap<RealEstateDto, RealEstate>().ReverseMap();

        CreateMap<CounterpartyDto, Counterparty>().ReverseMap();

        CreateMap<ApplicationDto, Domain.Entities.Application>().ReverseMap();

        CreateMap<ApplicationCreateDto, Domain.Entities.Application>().ReverseMap();

        CreateMap<CounterpartyCreateDto, Counterparty>().ReverseMap();

        CreateMap<RealEstateCreateDto, RealEstate>().ReverseMap();
    }
}