using Application.Dtos;
using AutoMapper;
using EstateAgency.Domain.Entities;

namespace Application.Mapper;

public class AppMapper : Profile
{

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