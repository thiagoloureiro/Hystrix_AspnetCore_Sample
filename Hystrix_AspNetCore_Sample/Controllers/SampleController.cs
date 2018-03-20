using Hystrix.Dotnet;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hystrix_AspNetCore_Sample.Controllers
{
    [Produces("application/json")]
    [Route("api/Sample")]
    public class SampleController : Controller
    {
        private readonly IHystrixCommand _hystrixCommand;

        public SampleController(IHystrixCommandFactory hystrixCommandFactory)
        {
            _hystrixCommand = hystrixCommandFactory.GetHystrixCommand("GrupoTeste", "ComandoTeste");
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _hystrixCommand.ExecuteAsync(
                async () =>
                {
                    var rnd = new Random();

                    if (rnd.Next(4) == 0) // Quando o valor for zero falha a retorna o fallback ao invés de cair na exception e retorar um server error
                    {
                        throw new Exception("Exception, pode logar, porém cairá no fallback e o processo continuará normalmente...");
                    }

                    return "Resultado Válido";
                },
                () => Task.FromResult("Resultado Fallback"),
                new CancellationTokenSource());

            return Ok($"Hello! Resultado: {result}");
        }
    }
}