using Agency.Application.Contracts.ContractRequests;
using Microsoft.AspNetCore.Mvc;

namespace Agency.Api.Host.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContractRequestController(IContractRequestService service, ILogger<ContractRequestController> logger)
    : CrudControllerBase<ContractRequestDto, ContractRequestCreateUpdateDto, int>(service, logger)
{
}
