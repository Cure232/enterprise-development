namespace Agency.Application.Contracts.ContractRequests;

/// <summary>
/// Сервис для работы с заявками
/// </summary>
public interface IContractRequestService : IApplicationService<ContractRequestDto, ContractRequestCreateUpdateDto, int>
{
}
