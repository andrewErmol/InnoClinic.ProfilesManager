using ProfilesManager.Domain.Entities;
using ProfilesManager.Domain.Parametrs;

namespace ProfilesManager.Domain.IRepositories
{
    public interface IDoctorsRepository
    {
        Task<IEnumerable<DoctorEntity>> GetDoctors(ParametersForGetDoctors parameters);
        Task<DoctorEntity> GetById(Guid id);
        Task<IEnumerable<Guid>> GetDoctorsIdsByOfficeId(Guid id);
        Task Create(DoctorEntity doctor);
        Task Delete(Guid id);
        Task Update(DoctorEntity doctor);
        Task UpdateDoctorStatus(Guid id, DoctorStatus doctorStatus);
        Task UpdateDoctorsOffice(IEnumerable<Guid> id, string address);
    }
}
