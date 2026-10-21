using ClientManagement.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClientManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestDbController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TestDbController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("status")]
        public async Task<IActionResult> CheckConnection()
        {
            bool canConnect = await _context.Database.CanConnectAsync();
            
            if (!canConnect)
            {
                return StatusCode(500, new { status = "Error", message = "Не вдалося підключитися до бази даних." });
            }

            int clientsCount = await _context.Clients.CountAsync();

            return Ok(new
            {
                status = "Success",
                message = "Підключення до LocalDB успішне!",
                totalClients = clientsCount
            });
        }
    }
}
