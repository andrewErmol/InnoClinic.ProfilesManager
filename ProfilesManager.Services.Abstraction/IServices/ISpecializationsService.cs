using ProfilesManager.Contracts.Models;

namespace ProfilesManager.Services.Abstraction.IServices
{
    public interface ISpecializationsService
    {
        Task<IEnumerable<Specialization>> GetSpecializations();
        Task<Specialization> GetSpecializationById(Guid id);
        Task<Guid> CreateSpecialization(Specialization specialization);
        Task DeleteSpecialization(Guid id);
        Task UpdateSpecialization(Guid id, Specialization specialization);
    }
}
