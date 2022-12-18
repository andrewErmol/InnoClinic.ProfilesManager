using Dapper;
using ProfilesManager.Domain.Entities;
using ProfilesManager.Domain.IRepositories;

namespace ProfilesManager.Persistence.DapperImplementation
{
    public class SpecializationsRepository : RepositoryBase<SpecializationEntity>, ISpecializationsRepository
    {
        private Type _entityType = typeof(SpecializationEntity);

        public SpecializationsRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<SpecializationEntity>> GetAll() =>
            await FindAll(_entityType);

        public async Task<SpecializationEntity> GetById(Guid id)
        {
            var specializationEntity = await FindByCondition(
                $"SELECT * FROM {GetTableName(_entityType)} WHERE Id = '{id}'"
                );

            return specializationEntity.SingleOrDefault();
        }

        public async Task Create(SpecializationEntity specialization)
        {
            var dict = new Dictionary<string, object>
            {
                { "@Id", specialization.Id },
                { "@Name", specialization.Name }
            };

            var parameters = new DynamicParameters(dict);

            string query = $"INSERT INTO {GetTableName(specialization.GetType())} VALUES" +
                "(@Id, @Name)";

            await Create(query, parameters);
        }

        public async Task Delete(Guid id) => await Delete(_entityType, id);

        public async Task Update(SpecializationEntity specialization)
        {
            var dict = new Dictionary<string, object>
            {
                { "@Id", specialization.Id },
                { "@Name", specialization.Name }
            };

            var parameters = new DynamicParameters(dict);

            string query = $"UPDATE {GetTableName(_entityType)} " +
                "SET Name = @Name " +
                "WHERE Id = @Id";

            await Update(query, parameters);
        }
    }
}
