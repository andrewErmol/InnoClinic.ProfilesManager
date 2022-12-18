using ProfilesManager.Domain.Entities;

namespace ProfilesManager.Domain.IRepositories
{
    public interface ISpecializationsRepository
    {
        Task<IEnumerable<SpecializationEntity>> GetAll();
        Task<SpecializationEntity> GetById(Guid id);
        Task Create(SpecializationEntity specialization);
        Task Delete(Guid id);
        Task Update(SpecializationEntity specialization);
    }
}
