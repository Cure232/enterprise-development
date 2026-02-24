using Agency.Application.Contracts.Protos;
using Agency.Application.Contracts.ContractRequests;
using AutoMapper;

namespace Agency.Generator.Grpc.Host.Grpc;

/// <summary>
/// Профиль AutoMapper для преобразования контрактных DTO в protobuf сообщения gRPC
/// </summary>
public sealed class AgencyGeneratorGrpcProfile : Profile
{
    /// <summary>
    /// Настройка правил маппинга между DTO приложения и сообщениями gRPC
    /// </summary>
    public AgencyGeneratorGrpcProfile()
    {
        CreateMap<ContractRequestCreateUpdateDto, ContractRequestCreateUpdateDtoMessage>()
            .ForMember(d => d.ContractRequestType, o => o.MapFrom(s => (int)s.ContractRequestType))
            .ForMember(d => d.Amount, o => o.MapFrom(s => (double)s.Amount))
            .ForMember(d => d.CreatedDate, o => o.MapFrom(s => s.CreatedDate.ToString("o")));
    }
}
