using Cowboy.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cowboy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FirearmController : ControllerBase
    {
        private readonly IFirearmService _service;

        public FirearmController(IFirearmService service)
        {
            _service = service;
        }

        // PUT: api/Firearm/ReloadTheGun/1
        [HttpPut("ReloadTheGun/{cowboyId}")]
        public async Task<IActionResult> ReloadTheGunAsync(int cowboyId)
        {
            var result = await _service.ReloadTheGunAsync(cowboyId);

            if((result?.Success ?? false) && (result?.Data ?? false))
            {
                return Ok(new ServiceResponse<bool>(result.Data));
            }
            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }

        // PUT: api/Firearm/ShootTheGun/1
        [HttpPut("ShootTheGun/{cowboyId}")]
        public async Task<IActionResult> ShootTheGunAsync(int cowboyId)
        {
            var result = await _service.ShootTheGunAsync(cowboyId);

            if ((result?.Success ?? false) && (result?.Data ?? false))
            {
                return Ok(new ServiceResponse<bool>(result.Data));
            }
            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }
    }
}
