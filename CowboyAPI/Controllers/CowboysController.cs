using CowboyAPI.Clients;
using CowboyAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq;

namespace CowboyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CowboysController : ControllerBase
    {
        private CowboyDbContext _context;
        private CowboyClient _client;

        public CowboysController(CowboyDbContext context, IOptions<AppSettings> options)
        {
            _context = context;
            _client = new CowboyClient(options);
        }

        // GET: api/Cowboys/GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var cowboys = await _context.Cowboys.ToListAsync();
            if (cowboys?.Any() ?? false)
            {
                return Ok(cowboys);                
            }
            return NoContent();
        }


        // GET: api/Cowboys/1
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var cowboy = await _context.Cowboys.FindAsync(id);
            if (cowboy == null)
            {
                return NotFound(new ApiResult<Cowboy>($"Cowboy (ID:{id}) not found."));
            }
            return Ok(cowboy);
        }

        // GET: api/Cowboys/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create()
        {
            try
            {
                var newCowboy = _client.GetCowboy()?.Result;

                if(newCowboy == null) { return BadRequest(new ApiResult<Cowboy>($"External API error on creating a new cowboy record.")); }
                var created = await _context.Cowboys.AddAsync(newCowboy);
                _context.SaveChanges();

                return CreatedAtAction(nameof(Create), new ApiResult<Cowboy>(created?.Entity));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                new ApiResult<Cowboy>("Error creating new cowboy record."));
            } 
        }

        // DELETE: api/Cowboys/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id <= 0) return BadRequest(new ApiResult<Cowboy>($"Invalid request Id."));

                var toDelete = await _context.Cowboys.FindAsync(id);

                if (toDelete == null) return NotFound(new ApiResult<Cowboy>($"Cowboy (ID:{id}) not found."));

                _context.Cowboys.Remove(toDelete);

                return Ok(new ApiResult<Cowboy>(toDelete));

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                new ApiResult<Cowboy>("Error deleting cowboy record."));
            }
        }

        // Patch cowboy

        // Shoot the gun
        // Reload the gun
        // Combat
    }
}
