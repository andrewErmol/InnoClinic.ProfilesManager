using ProfilesManager.Domain.Entities;

namespace ProfilesManager.Domain.IRepositories
{
    public interface IDoctorsRepository
    {
        Task<IEnumerable<DoctorEntity>> GetAll();
        Task<DoctorEntity> GetById(Guid id);
        Task<DoctorEntity> GetByName(string name);
        Task<DoctorEntity> GetByOffice(Guid officeId);
        Task<DoctorEntity> GetBySpecialization(string specializationName);
        Task Create(DoctorEntity doctor);
        Task Delete(Guid id);
        Task Update(DoctorEntity doctor);
        Task UpdateDoctorStatus(Guid id, int doctorStatus);
    }
}
