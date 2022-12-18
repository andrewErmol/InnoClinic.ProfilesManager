using Microsoft.AspNetCore.Mvc;
using ProfilesManager.Services.Abstraction.IServices;

namespace ProfilesManager.Presentation.Controllers
{
    [Route("api/migrations")]
    [ApiController]
    public class MigrationsController : ControllerBase
    {
        private readonly IMigrationsService _migrationsService;

        public MigrationsController(IMigrationsService migrationsService)
        {
            _migrationsService = migrationsService;
        }

        [HttpPost]
        public IActionResult CreateTables()
        {
            _migrationsService.CreateTables();

            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteTables()
        {
            _migrationsService.DeleteTables();

            return NoContent();
        }

        [HttpDelete("{data}")]
        public IActionResult ClearTables()
        {
            _migrationsService.ClearTables();

            return NoContent();
        }
    }
}
