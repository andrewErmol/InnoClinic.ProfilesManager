using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProfilesManager.Contracts;
using ProfilesManager.Contracts.Models;
using ProfilesManager.Contracts.RequestEntity;
using ProfilesManager.Services.Abstraction.IServices;

namespace ProfilesManager.Presentation.Controllers
{
    [Route("api/")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;

        public DoctorsController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        [HttpGet("admin/doctors")]
        public async Task<IActionResult> Doctors()
        {
            return Ok(await _serviceManager.DoctorsService.GetDoctors()); 
        }

        [HttpGet("patient/doctors")]
        public async Task<IActionResult> DoctorsForPatient()
        {
            return Ok(await _serviceManager.DoctorsService.GetDoctorsForPatient());
        }

        [HttpGet("admin/doctors/{id}")]
        public async Task<IActionResult> DoctorById(Guid id)
        {
            var doctor = await _serviceManager.DoctorsService.GetDoctorById(id);

            return Ok(doctor);
        }

        [HttpGet("admin/doctors/name")]
        public async Task<IActionResult> DoctorByName([FromQuery] DoctorNameToFilter name)
        {
            var doctor = await _serviceManager.DoctorsService.GetDoctorByName(name.DoctorName);

            return Ok(doctor);
        }

        [HttpGet("patient/doctors/{id}")]
        public async Task<IActionResult> DoctorForPatient(Guid id)
        {
            var doctor = await _serviceManager.DoctorsService.GetDoctorByIdForPatient(id);

            return Ok(doctor);
        }

        [HttpGet("admin/doctors/office")]
        public async Task<IActionResult> DoctorByOffice([FromQuery] OfficeIdToFilter office)
        {
            var doctor = await _serviceManager.DoctorsService.GetDoctorByOffice(Guid.Parse(office.OfficeId));

            return Ok(doctor);
        }

        [HttpGet("admin/doctors/specialization")]
        public async Task<IActionResult> DoctorBySpecialization([FromQuery] DoctorSpecializationToFilter specialization)
        {
            var doctor = await _serviceManager.DoctorsService.GetDoctorBySpecialization(specialization.SpecializationName);

            return Ok(doctor);
        }

        [HttpPost("doctors")]
        public async Task<IActionResult> CreateDoctor([FromBody] DoctorForRequest doctorForRequest)
        {
            var doctor = _mapper.Map<Doctor>(doctorForRequest);

            var doctorToReturn = await _serviceManager.DoctorsService.CreateDoctor(doctor);

            return Ok(doctorToReturn.Id);
        }

        [HttpPut("doctors/{id}")]
        public async Task<IActionResult> UpdateDoctor(Guid id, [FromBody] DoctorForRequest doctorForRequest)
        {
            var doctor = _mapper.Map<Doctor>(doctorForRequest);

            await _serviceManager.DoctorsService.UpdateDoctor(id, doctor);

            return NoContent();
        }

        [HttpPatch("doctors/{id}")]
        public async Task<IActionResult> UpdateDoctorStatus(Guid id, [FromBody] int doctorStatus)
        {
            await _serviceManager.DoctorsService.UpdateDoctorStatus(id, doctorStatus);

            return NoContent();
        }

        [HttpDelete("doctors/{id}")]
        public async Task<IActionResult> DeleteDoctor(Guid id)
        {
            await _serviceManager.DoctorsService.DeleteDoctor(id);

            return NoContent();
        }
    }
}
