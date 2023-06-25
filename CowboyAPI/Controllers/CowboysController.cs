using Cowboy.Repository.Models;
using Cowboy.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CowboyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CowboysController : ControllerBase
    {
        private readonly ICowboyService _service;

        public CowboysController(ICowboyService service)
        {
            _service = service;
        }

        // GET: api/Cowboys/GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var cowboys = await _service.GetAllCowboysAsync();
            if (cowboys?.Success ?? false)
            {
                if(cowboys.Data?.Any() ?? false) return Ok(cowboys);
                else return NoContent();
            }
            return StatusCode(StatusCodes.Status500InternalServerError, cowboys);
        }


        // GET: api/Cowboys/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var cowboy = await _service.GetCowboyByIdAsync(id);
            if (cowboy?.Success ?? false)
            {
                if (cowboy.Data != null) return Ok(cowboy);
                else return NoContent();
            }
            return StatusCode(StatusCodes.Status500InternalServerError, cowboy);
        }

        // POST: api/Cowboys/Create
        [HttpPost("Create")]
        public async Task<IActionResult> CreateAsync()
        {
            var cowboy = await _service.CreateCowboyAsync();

            if (cowboy?.Success ?? false)
            {
                if (cowboy.Data != null) return Ok(cowboy);
                else return NoContent();
            }
            return StatusCode(StatusCodes.Status500InternalServerError, cowboy);
        }

        // DELETE: api/Cowboys/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id <= 0) return BadRequest(new ServiceResponse<CowboyModel>($"Invalid request Id.", false));

            var response = await _service.DeleteCowboyAsync(id);

            if (response?.Success ?? false)
            {
                return Ok(new ServiceResponse<CowboyModel>());
            }
            return NotFound(new ServiceResponse<CowboyModel>($"Cowboy (ID:{id}) not found.", false));
        }

        // PATCH: api/Cowboys/1
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchAsync([FromRoute] int id, [FromBody] JsonPatchDocument cowboyDocument)
        {
            if (id <= 0) return BadRequest(new ServiceResponse<CowboyModel>($"Invalid request Id.", false));

            var updatedCowboy = await _service.UpdateCowboyPatchAsync(id, cowboyDocument);

            if (updatedCowboy?.Success ?? false)
            {
                return Ok(updatedCowboy);
            }
            return NotFound(new ServiceResponse<CowboyModel>($"Cowboy (ID:{id}) not found.", false));
        }
    }
}
