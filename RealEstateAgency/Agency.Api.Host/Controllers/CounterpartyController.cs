using Agency.Application.Contracts.Counterparties;
using Microsoft.AspNetCore.Mvc;

namespace Agency.Api.Host.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CounterpartyController(ICounterpartyService service, ILogger<CounterpartyController> logger)
    : CrudControllerBase<CounterpartyDto, CounterpartyCreateUpdateDto, int>(service, logger)
{
}
