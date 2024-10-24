using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace API_HangFire.Controllers
{
    [Route("api/[controller]")]
    public class ServicoController : Controller
    {
        private readonly ILogger<ServicoController> _logger;

        public ServicoController(ILogger<ServicoController> logger)
        {
            _logger = logger;
        }

        [HttpGet("RodaUmaVez")]
        // Os trtabalhos de despedir e esquecer sãoexecutados apenas uma vez e quase imiediatamente após a criação.
        public IActionResult RodaUmaVez()
        {
            try
            {
                var jobFireForget = BackgroundJob.Enqueue(() => Acao.RegistrarMensagem("RodaUmaVez"));
            }
            catch (Exception EX)
            {

            }
            return Ok();
        }


        // Os trabalhos atrasados também são executados uma vez, mas não imediatamente, após um determinado intervalo de tempo.
        [HttpGet("RodarAposTempo")]
        public IActionResult RodarAposTempo()
        {
            var jobDelayed = BackgroundJob.Schedule(() => Acao.RegistrarMensagem("RodarAposTempo"), TimeSpan.FromSeconds(10));

            return Ok();
        }


        // As contiuações são executadas quando seu trabalho pai foi concluído. Caso o site capotar, ele continua rodando.
        [HttpGet("RodarAposTempoContinuo")]
        public IActionResult RodarAposTempoContinuo()
        {
            var jobDelayed = BackgroundJob.Schedule(() => Acao.RegistrarMensagem("RodaUmaVez"), TimeSpan.FromSeconds(10));

            BackgroundJob.ContinueJobWith(jobDelayed, () => Acao.RegistrarMensagem("RodarAposTempoContinuo"));

            return Ok();
        }


        //Trabalhos recorrentes são acionados muitas vezes no agendamento CRON especificado.
        [HttpGet("RodarSempre")]
        public IActionResult RodarSempre()
        {
            RecurringJob.AddOrUpdate(() => Acao.RegistrarMensagem("RodarSempre"), Cron.Minutely);

            return Ok();
        }
    }
}
