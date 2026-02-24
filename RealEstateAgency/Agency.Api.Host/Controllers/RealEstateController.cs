using Agency.Application.Contracts.RealEstates;
using Microsoft.AspNetCore.Mvc;

namespace Agency.Api.Host.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RealEstateController(IRealEstateService service, ILogger<RealEstateController> logger)
    : CrudControllerBase<RealEstateDto, RealEstateCreateUpdateDto, int>(service, logger)
{
}
