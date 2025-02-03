using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SomeController : ControllerBase
    {
        [HttpGet("sync")]
        public IActionResult GetSync()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Thread.Sleep(1000);
            Console.WriteLine("coneected BD");

            Thread.Sleep(1000);
            Console.WriteLine("Envio de mail terminado");

            Console.WriteLine("todo ha terminado");

            stopwatch.Stop();



            return Ok(($"Tiempo total: {stopwatch.ElapsedMilliseconds}ms"));
        }

        [HttpGet("async")]
        public async Task<IActionResult> GetAsync()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Start();

            var task1 = new Task<int>(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("coneccion a bd terminada");
                return 8;
            });

            var task2 = new Task<int>(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("envio de email");
                return 2;
            });

            task1.Start();
            task2.Start();

            Console.WriteLine("hago otra");

            var result1 = await task1;
            var result2 = await task2;

            Console.WriteLine("termino todo");
            stopwatch.Stop();

            return Ok(result1 + result2 + " " + $"Tiempo total: {stopwatch.ElapsedMilliseconds}ms");
        }
    }
}
