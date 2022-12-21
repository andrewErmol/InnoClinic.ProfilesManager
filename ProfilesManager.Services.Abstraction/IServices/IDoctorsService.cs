using ProfilesManager.Contracts.Models;

namespace ProfilesManager.Services.Abstraction.IServices
{
    public interface IDoctorsService
    {
        Task<IEnumerable<Doctor>> GetDoctors();
        Task<IEnumerable<DoctorForPatient>> GetDoctorsForPatient();
        Task<Doctor> GetDoctorById(Guid id);
        Task<IEnumerable<Doctor>> GetDoctorByName(string name);
        Task<DoctorForPatient> GetDoctorByIdForPatient(Guid id);
        Task<IEnumerable<Doctor>> GetDoctorByOffice(Guid officeId);
        Task<IEnumerable<Doctor>> GetDoctorBySpecialization(string specializationName);
        Task<Doctor> CreateDoctor(Doctor doctor);
        Task DeleteDoctor(Guid id);
        Task UpdateDoctor(Guid id, Doctor doctor);
        Task UpdateDoctorStatus(Guid id, int doctorStatus);
    }
}
