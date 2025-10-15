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
        CreateMap<RealEstateDto, RealEstate>();
        CreateMap<RealEstate, RealEstateDto>();

        CreateMap<CounterpartyDto, Counterparty>();
        CreateMap<Counterparty, CounterpartyDto>();

        CreateMap<ApplicationDto, EstateAgency.Domain.Entities.Application>();
        CreateMap<EstateAgency.Domain.Entities.Application, ApplicationDto>();
    }
}