using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfilesManager.Contracts.Models;
using ProfilesManager.Domain.Parametrs;
using ProfilesManager.Presentation.RequestEntity;
using ProfilesManager.Services.Abstraction.IServices;
using System.IdentityModel.Tokens.Jwt;

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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetDoctors([FromQuery] ParametersForGetDoctors parameters)
        {
            var token = (JwtSecurityToken)new JwtSecurityTokenHandler().ReadToken("eyJhbGciOiJSUzI1NiIsImtpZCI6IkY5NTBBMDlGMkU1NDA5NjQ1REQwNTE4Q0ZGMDM3QTk3IiwidHlwIjoiYXQrand0In0.eyJuYmYiOjE2NzIxNDUyMjgsImV4cCI6MTY3MjE0ODgyOCwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzEzMCIsImNsaWVudF9pZCI6Ik9mZmljZXNNYW5hZ2VyLkFQSSIsInN1YiI6IjYxYzMwYTQyLTM3ZTYtNDczNi1iNDJkLWE3Nzk1MjIxM2M5YSIsImF1dGhfdGltZSI6MTY3MjE0NTIyOCwiaWRwIjoibG9jYWwiLCJqdGkiOiI2Rjg2NTY0ODA4REZCMDM4NkU0NkExMUZDRUU3QkFCRCIsImlhdCI6MTY3MjE0NTIyOCwic2NvcGUiOlsiT2ZmaWNlc01hbmFnZXIuQVBJIl0sImFtciI6WyJwd2QiXX0.yqcUIQmjkCdu3bz8nUOegVKeuubwnDtbJ3w_fGvuA5jZHnTLBV0lxgwcMLJksAs-kTb1A_1rvxhSVejjTUASurSq8yIN7R6wqpGT6-8P_7hNJSE8DhCC0rm0mEooZ_giEXSk2X2ZoFFEkL3meB4_FxX9ZHEMujnBNwLbhfkcV7N_UYK7gbQ0eQQ3lHRXgOK5KqaRKj0l_vAxJEJUlLCUYVl6bDFQfplmtlFAOr6-LhZfYtNXfUOcBkQnhE4k6AY_dtQEk64mwkgyTdJjXSm9qhcGylJGPnBVPa-dSIUVDOcMv5NKecZe2NZ0fBekmhCsj6upnvNkcuKcJg_wft8hzg");

            if (User.IsInRole("Admin"))
            {
                return Ok(await _serviceManager.DoctorsService.GetDoctors(parameters));
            }            

            return Ok(await _serviceManager.DoctorsService.GetDoctorsForPatient(parameters));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctor(Guid id)
        {
            if (User.IsInRole("Admin"))
            {
                return Ok(await _serviceManager.DoctorsService.GetDoctorById(id));
            }
            else
            {
                return Ok(await _serviceManager.DoctorsService.GetDoctorByIdForPatient(id));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDoctor([FromBody] DoctorForRequest doctorForRequest)
        {
            var doctor = _mapper.Map<Doctor>(doctorForRequest);

            var createdDoctorId = await _serviceManager.DoctorsService.CreateDoctor(doctor);

            return CreatedAtAction(nameof(GetDoctors), new { id = createdDoctorId });
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
