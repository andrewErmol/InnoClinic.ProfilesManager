using ProfilesManager.Domain.Entities;

namespace ProfilesManager.Domain.IRepositories
{
    public interface IPatientsRepository
    {
        Task<IEnumerable<PatientEntity>> GetAll();
        Task<PatientEntity> GetById(Guid id);
        Task Create(PatientEntity patient);
        Task Delete(Guid id);
        Task Update(PatientEntity patient);
    }
}
