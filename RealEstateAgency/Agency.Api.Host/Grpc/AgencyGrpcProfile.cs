using Agency.Application.Contracts.Protos;
using Agency.Application.Contracts.ContractRequests;
using AutoMapper;

namespace Agency.Api.Host.Grpc;

/// <summary>
/// Профиль AutoMapper для преобразования protobuf сообщений gRPC в контрактные DTO приложения
/// </summary>
public class AgencyGrpcProfile : Profile
{
    /// <summary>
    /// Настройка правил маппинга между сообщениями gRPC и DTO используемыми в прикладном слое
    /// </summary>
    public AgencyGrpcProfile()
    {
        CreateMap<ContractRequestCreateUpdateDtoMessage, ContractRequestCreateUpdateDto>()
            .ForCtorParam("ContractRequestType", o => o.MapFrom(s => (Domain.Model.ContractRequestType)s.ContractRequestType))
            .ForCtorParam("Amount", o => o.MapFrom(s => (decimal)s.Amount))
            .ForCtorParam("CreatedDate", o => o.MapFrom(s => DateTime.Parse(s.CreatedDate)));
    }
}
