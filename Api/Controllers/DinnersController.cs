using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
public class DinnersController : ApiController
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(Enumerable.Empty<object>());
    }
}
