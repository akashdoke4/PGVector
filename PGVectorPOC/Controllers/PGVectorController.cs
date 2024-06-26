using Microsoft.AspNetCore.Mvc;
using PGVectorPOC.Services.Interface;

namespace PGVectorPOC.Controllers
{
    [ApiController]
    [Route("/api")]
    public class PGVectorController : ControllerBase
    {
        private readonly ILogger<PGVectorController> _logger;
        private readonly IService _service;

        public PGVectorController(ILogger<PGVectorController> logger, IService service)
        {
            _logger = logger;
            _service = service;
        }


        [HttpPost(Name = "CreateTable")]
        public async Task<ActionResult> Post()
        {
            try
            {
                await _service.CreateTable();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while storing the embeddings Error : {Error} StackTrace : {StackTrace} DateTime : {DateTime}", ex.Message, ex.StackTrace, DateTime.UtcNow);
                return new StatusCodeResult(500);

            }
        }
    }
}
