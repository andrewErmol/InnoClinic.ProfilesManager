using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProfilesManager.Contracts.Models;
using ProfilesManager.Contracts.RequestEntity;
using ProfilesManager.Services.Abstraction.IServices;

namespace ProfilesManager.Presentation.Controllers
{
    [Route("api/patients")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;

        public PatientsController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Patients()
        {
            return Ok(await _serviceManager.PatientsService.GetPatients());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Patient(Guid id)
        {
            var patient = await _serviceManager.PatientsService.GetPatientById(id);

            return Ok(patient);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePatient([FromBody] PatientForRequest patientForRequest)
        {
            var patient = _mapper.Map<Patient>(patientForRequest);

            var patientToReturn = await _serviceManager.PatientsService.CreatePatient(patient);

            return Ok(patientToReturn.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(Guid id, [FromBody] PatientForRequest patientForRequest)
        {
            var patient = _mapper.Map<Patient>(patientForRequest);

            await _serviceManager.PatientsService.UpdatePatient(id, patient);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(Guid id)
        {
            await _serviceManager.PatientsService.DeletePatient(id);

            return NoContent();
        }
    }
}
