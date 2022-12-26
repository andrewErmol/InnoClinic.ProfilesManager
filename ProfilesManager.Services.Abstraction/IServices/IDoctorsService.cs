using ProfilesManager.Contracts.Models;
using ProfilesManager.Domain.Parametrs;

namespace ProfilesManager.Services.Abstraction.IServices
{
    public interface IDoctorsService
    {
        Task<IEnumerable<Doctor>> GetDoctors(ParametersForGetDoctors parameters);
        Task<IEnumerable<DoctorForPatient>> GetDoctorsForPatient(ParametersForGetDoctors parameters);
        Task<Doctor> GetDoctorById(Guid id);
        Task<DoctorForPatient> GetDoctorByIdForPatient(Guid id);
        Task<Guid> CreateDoctor(Doctor doctor);
        Task DeleteDoctor(Guid id);
        Task UpdateDoctor(Guid id, Doctor doctor);
        Task UpdateDoctorStatus(Guid id, string doctorStatus);
    }
}
