using Ecommerce3.Contracts.QueryRepositories;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce3.Admin.Controllers.API;

[Route("api/[controller]")]
[ApiController]
public class UnitOfMeasuresController(IUnitOfMeasureQueryRepository unitOfMeasureQueryRepository) : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        var uom = await unitOfMeasureQueryRepository.GetByUnitOfMeasureIdAsync(id, cancellationToken);
        if (uom is null) return NotFound("Unit of measure not found.");
        
        return Ok(uom);
    }
}