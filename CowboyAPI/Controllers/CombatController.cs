using Cowboy.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cowboy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CombatController : ControllerBase
    {
        private readonly ICombatService _service;

        public CombatController(ICombatService service)
        {
            _service = service;
        }

        // PUT: api/Firearm/Combat?cowboyOneId=1&cowboyTwoId=2
        [HttpPut("Combat")]
        public async Task<IActionResult> CombatAsync(int cowboyOneId, int cowboyTwoId)
        {
            if (cowboyOneId == cowboyTwoId) return BadRequest(new ServiceResponse<string>("Cowboy cannot combat against himself.", false));

            var result = await _service.CombatAsync(cowboyOneId, cowboyTwoId);

            if (result?.Success ?? false)
            {
                return Ok(result);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }
    }   
}
