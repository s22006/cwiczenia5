using Microsoft.AspNetCore.Mvc;
using zd7.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace zd7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {

        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        // DELETE api/<ClientsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _clientService.RemoveClientById(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
            return Ok($"Client with id {id} removed");
        }
    }
}
