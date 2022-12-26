using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProfilesManager.Contracts.Models;
using ProfilesManager.Presentation.RequestEntity;
using ProfilesManager.Services.Abstraction.IServices;

namespace ProfilesManager.Presentation.Controllers
{
    [Route("api/specializations")]
    [ApiController]
    public class SpecializationsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;

        public SpecializationsController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Specializations()
        {
            return Ok(await _serviceManager.SpecializationsService.GetSpecializations());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Specialization(Guid id)
        {
            var specialization = await _serviceManager.SpecializationsService.GetSpecializationById(id);

            return Ok(specialization);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSpecialization([FromBody] SpecializationForRequest specializationForRequest)
        {
            var specialization = _mapper.Map<Specialization>(specializationForRequest);

            var createdSpecializationId = await _serviceManager.SpecializationsService.CreateSpecialization(specialization);

            return CreatedAtAction(nameof(Specializations), new { id = createdSpecializationId });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSpecialization(Guid id, [FromBody] SpecializationForRequest specializationForRequest)
        {
            var specialization = _mapper.Map<Specialization>(specializationForRequest);

            await _serviceManager.SpecializationsService.UpdateSpecialization(id, specialization);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpecialization(Guid id)
        {
            await _serviceManager.SpecializationsService.DeleteSpecialization(id);

            return NoContent();
        }
    }
}
