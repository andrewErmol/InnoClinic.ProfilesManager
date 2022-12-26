using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProfilesManager.Contracts.Models;
using ProfilesManager.Domain.Parametrs;
using ProfilesManager.Presentation.RequestEntity;
using ProfilesManager.Services.Abstraction.IServices;

namespace ProfilesManager.Presentation.Controllers
{
    [Route("api/doctors")]
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

        [HttpGet]
        public async Task<IActionResult> Doctors([FromQuery] ParametersForGetDoctors parameters)
        {
            if (User.IsInRole("Admin"))
            {
                return Ok(await _serviceManager.DoctorsService.GetDoctors(parameters));
            }            

            return Ok(await _serviceManager.DoctorsService.GetDoctorsForPatient(parameters));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Doctor(Guid id)
        {
            if (User.IsInRole("Admin"))
            {
                var doctor = await _serviceManager.DoctorsService.GetDoctorById(id);
                return Ok(doctor);
            }
            else
            {
                var doctor = await _serviceManager.DoctorsService.GetDoctorByIdForPatient(id);
                return Ok(doctor);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDoctor([FromBody] DoctorForRequest doctorForRequest)
        {
            var doctor = _mapper.Map<Doctor>(doctorForRequest);

            var createdDoctorId = await _serviceManager.DoctorsService.CreateDoctor(doctor);

            return CreatedAtAction(nameof(Doctors), new { id = createdDoctorId });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDoctor(Guid id, [FromBody] DoctorForRequest doctorForRequest)
        {
            var doctor = _mapper.Map<Doctor>(doctorForRequest);

            await _serviceManager.DoctorsService.UpdateDoctor(id, doctor);

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateDoctorStatus(Guid id, [FromQuery] string status)
        {
            await _serviceManager.DoctorsService.UpdateDoctorStatus(id, status);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(Guid id)
        {
            await _serviceManager.DoctorsService.DeleteDoctor(id);

            return NoContent();
        }
    }
}
