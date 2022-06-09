using Microsoft.AspNetCore.Mvc;
using zd7.Models.Dtos;
using zd7.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace zd7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ITripService _tripService;

        public TripsController(ITripService tripService)
        {
            _tripService = tripService;
        }

        // GET: api/<TripsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _tripService.GetAllTripsAsync();
            return Ok(result);
        }

        // POST api/<TripsController>/5/clients
        [HttpPost("{id}/clients")]
        public async Task<IActionResult> Post(int id, [FromBody] AssignClientToTripDto request)
        {
            try
            {
                await _tripService.AssignClientToTrip(request);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(new { message = "client assigned to trip" });
        }

        
    }
}
