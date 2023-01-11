using ProfilesManager.Domain.Entities;

namespace ProfilesManager.Domain.IRepositories
{
    public interface IReceptionistsRepository
    {
        Task<IEnumerable<ReceptionistEntity>> GetAll();
        Task<ReceptionistEntity> GetById(Guid id);
        Task<IEnumerable<Guid>> GetReceptionistsIdsByOfficeId(Guid id);
        Task Create(ReceptionistEntity receptionist);
        Task Delete(Guid id);
        Task Update(ReceptionistEntity receptionist);
        Task UpdateReceptionistsOffice(IEnumerable<Guid> id, string address);
    }
}
