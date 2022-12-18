using Dapper;

namespace ProfilesManager.Domain.IRepositories
{
    public interface IRepositoryBase<T>
    {
        Task<IEnumerable<T>> FindAll(Type entityType);
        Task<IEnumerable<T>> FindByCondition(string query);
        Task Create(string query, DynamicParameters parameters);
        Task Update(string query, DynamicParameters parameters);
        Task Delete(Type entityType, Guid id);
    }
}
