using ProfilesManager.Domain.Entities;

namespace ProfilesManager.Domain.IRepositories
{
    public interface IDoctorsRepository
    {
        Task<IEnumerable<DoctorEntity>> GetAll();
        Task<DoctorEntity> GetById(Guid id);
        Task<IEnumerable<DoctorEntity>> GetByName(string name);
        Task<IEnumerable<DoctorEntity>> GetByOffice(Guid officeId);
        Task<IEnumerable<DoctorEntity>> GetBySpecialization(string specializationName);
        Task Create(DoctorEntity doctor);
        Task Delete(Guid id);
        Task Update(DoctorEntity doctor);
        Task UpdateDoctorStatus(Guid id, DoctorStatus doctorStatus);
    }
}
