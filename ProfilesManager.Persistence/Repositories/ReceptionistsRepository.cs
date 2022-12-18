using Dapper;
using ProfilesManager.Domain.Entities;
using ProfilesManager.Domain.IRepositories;

namespace ProfilesManager.Persistence.DapperImplementation
{
    public class ReceptionistsRepository : RepositoryBase<ReceptionistEntity>, IReceptionistsRepository
    {
        private Type _entityType = typeof(ReceptionistEntity);

        public ReceptionistsRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<ReceptionistEntity>> GetAll() =>
            await FindAll(_entityType);

        public async Task<ReceptionistEntity> GetById(Guid id)
        {
            var receptionistEntity = await FindByCondition(
                $"SELECT * FROM {GetTableName(_entityType)} WHERE Id = '{id}'"
                );

            return receptionistEntity.SingleOrDefault();
        }

        public async Task Create(ReceptionistEntity receptionist)
        {
            var dict = new Dictionary<string, object>
            {
                { "@Id", receptionist.Id },
                { "@FirstName", receptionist.FirstName },
                { "@LastName", receptionist.LastName },
                { "@MiddleName", receptionist.MiddleName },
                { "@DateOfBirth", receptionist.DateOfBirth },
                { "@OfficeId", receptionist.OfficeId },
                { "@AccountId", receptionist.AccountId }
            };

            var parameters = new DynamicParameters(dict);

            string query = $"INSERT INTO {GetTableName(receptionist.GetType())} VALUES" +
                "(@Id, @FirstName, @LastName, @MiddleName, @DateOfBirth, @OfficeId, @AccountId)";

            await Create(query, parameters);
        }

        public async Task Delete(Guid id) => await Delete(_entityType, id);

        public async Task Update(ReceptionistEntity receptionist)
        {
            var dict = new Dictionary<string, object>
            {
                { "@Id", receptionist.Id },
                { "@FirstName", receptionist.FirstName },
                { "@LastName", receptionist.LastName },
                { "@MiddleName", receptionist.MiddleName },
                { "@DateOfBirth", receptionist.DateOfBirth },
                { "@OfficeId", receptionist.OfficeId },
                { "@AccountId", receptionist.AccountId }
            };

            var parameters = new DynamicParameters(dict);

            string query = $"UPDATE {GetTableName(_entityType)} " +
                "SET FirstName = @FirstName, LastName = @LastName, MiddleName = @MiddleName, DateOfBirth = @DateOfBirth, OfficeId = @OfficeId AccountId = @AccountId " +
                "WHERE Id = @Id";

            await Update(query, parameters);
        }
    }
}
