using ProfilesManager.Contracts.Models;

namespace ProfilesManager.Services.Abstraction.IServices
{
    public interface IReceptionistsService
    {
        Task<IEnumerable<Receptionist>> GetReceptionists();
        Task<Receptionist> GetReceptionistById(Guid id);
        Task<Guid> CreateReceptionist(Receptionist receptionist);
        Task DeleteReceptionist(Guid id);
        Task UpdateReceptionist(Guid id, Receptionist receptionist);
        Task UpdateReceptionistsAddress(Guid id, string doctorStatus);
    }
}
