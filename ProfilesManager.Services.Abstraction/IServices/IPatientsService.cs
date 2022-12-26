using ProfilesManager.Contracts.Models;

namespace ProfilesManager.Services.Abstraction.IServices
{
    public interface IPatientsService
    {
        Task<IEnumerable<Patient>> GetPatients();
        Task<Patient> GetPatientById(Guid id);
        Task<Guid> CreatePatient(Patient patient);
        Task DeletePatient(Guid id);
        Task UpdatePatient(Guid id, Patient patient);
    }
}
