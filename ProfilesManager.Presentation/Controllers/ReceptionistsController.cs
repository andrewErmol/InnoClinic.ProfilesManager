using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProfilesManager.Contracts.Models;
using ProfilesManager.Presentation.RequestEntity;
using ProfilesManager.Services.Abstraction.IServices;

namespace ProfilesManager.Presentation.Controllers
{
    [Route("api/receptionists")]
    [ApiController]
    public class ReceptionistsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;

        public ReceptionistsController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Receptionists()
        {
            return Ok(await _serviceManager.ReceptionistsService.GetReceptionists());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Receptionist(Guid id)
        {
            var receptionist = await _serviceManager.ReceptionistsService.GetReceptionistById(id);

            return Ok(receptionist);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReceptionist([FromBody] ReceptionistForRequest receptionistForRequest)
        {
            var receptionist = _mapper.Map<Receptionist>(receptionistForRequest);

            var createdReceptionistId = await _serviceManager.ReceptionistsService.CreateReceptionist(receptionist);

            return CreatedAtAction(nameof(Receptionists), new { id = createdReceptionistId });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReceptionist(Guid id, [FromBody] ReceptionistForRequest receptionistForRequest)
        {
            var receptionist = _mapper.Map<Receptionist>(receptionistForRequest);

            await _serviceManager.ReceptionistsService.UpdateReceptionist(id, receptionist);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReceptionist(Guid id)
        {
            await _serviceManager.ReceptionistsService.DeleteReceptionist(id);

            return NoContent();
        }
    }
}
