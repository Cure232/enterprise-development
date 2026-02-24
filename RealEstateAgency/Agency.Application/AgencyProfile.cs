using Agency.Application.Contracts.ContractRequests;
using Agency.Application.Contracts.Counterparties;
using Agency.Application.Contracts.RealEstates;
using Agency.Domain.Model;
using AutoMapper;

namespace Agency.Application;

/// <summary>
/// Профиль маппинга AutoMapper для риэлторского агентства
/// </summary>
public class AgencyProfile : Profile
{
    public AgencyProfile()
    {
        CreateMap<Counterparty, CounterpartyDto>();
        CreateMap<CounterpartyCreateUpdateDto, Counterparty>();

        CreateMap<RealEstate, RealEstateDto>();
        CreateMap<RealEstateCreateUpdateDto, RealEstate>();

        CreateMap<ContractRequest, ContractRequestDto>()
            .ForMember(d => d.CounterpartyId, o => o.MapFrom(s => s.CounterpartyId))
            .ForMember(d => d.RealEstateId, o => o.MapFrom(s => s.RealEstateId));
        CreateMap<ContractRequestCreateUpdateDto, ContractRequest>()
            .ForMember(d => d.Counterparty, o => o.Ignore())
            .ForMember(d => d.RealEstate, o => o.Ignore());
    }
}
