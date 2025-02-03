using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OperationController : ControllerBase
{
    [HttpGet]
    public decimal Add(decimal a, decimal b)
    {
        return a + b;
    }
}

public class Numbers
{
    
}