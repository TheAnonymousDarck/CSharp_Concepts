using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandomController(
        [FromKeyedServices("randomServiceSingleton")] IRandomService randomServiceSingleton,
        [FromKeyedServices("randomServiceScoped")]
        IRandomService randomServiceScoped,
        [FromKeyedServices("randomServiceTransient")]
        IRandomService randomServiceTransient,
        [FromKeyedServices("random2ServiceSingleton")]
        RandomService random2ServiceSingleton,
        [FromKeyedServices("random2ServiceScoped")]
        RandomService random2ServiceScoped,
        [FromKeyedServices("random2ServiceTransient")]
        RandomService random2ServiceTransient)
        : ControllerBase
    {
        [HttpGet]
        public ActionResult<Dictionary<string, int>> Get()
        {
            var result = new Dictionary<string, int>();
            
            result.Add("randomServiceSingleton", randomServiceSingleton.value);
            result.Add("randomServiceScoped", randomServiceScoped.value);
            result.Add("randomServiceTransient", randomServiceTransient.value);
            
            result.Add("random2ServiceSingleton", random2ServiceSingleton.value);
            result.Add("random2ServiceScoped", random2ServiceScoped.value);
            result.Add("random2ServiceTransient", random2ServiceTransient.value);
            
            return result;

        }
    }
}
