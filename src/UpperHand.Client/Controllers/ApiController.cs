namespace UpperHand.MVC.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using UpperHand.Services;

    [Route("api/hand")]
    [ApiController]
    [Produces("application/json")]
    public class ApiController : ControllerBase
    {

        private readonly IHandService _service;

        public ApiController(IHandService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try {
                /* This would ideally use a command pattern, maybe with mediatr */
                var result = await _service.RequestDealtHand();

                return Ok(result);
            }
            catch (Exception ex) {
                return BadRequest(ex);
            }
        }
    }
}
