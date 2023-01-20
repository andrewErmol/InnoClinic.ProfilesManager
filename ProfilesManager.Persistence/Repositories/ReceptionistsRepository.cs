using Dapper;
using ProfilesManager.Domain.Entities;
using ProfilesManager.Domain.IRepositories;
using System.Data;
using System.Data.SqlClient;

namespace ProfilesManager.Persistence.Repositories
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

        public async Task<IEnumerable<Guid>> GetReceptionistsIdsByOfficeId(Guid id)
        {
            var query = $"SELECT Id FROM {GetTableName(_entityType)} " +
                $"WHERE Receptionists.OfficeId = '{id}'";

            IEnumerable<Guid> receprionistsIds;

            using (IDbConnection db = new SqlConnection(connectionString))
            {
                receprionistsIds = await db.QueryAsync<Guid>(query);
            }

            return receprionistsIds;
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
                { "@AccountId", receptionist.AccountId },
                { "@Address", receptionist.Address }
            };

            var parameters = new DynamicParameters(dict);

            string query = $"INSERT INTO {GetTableName(receptionist.GetType())} VALUES" +
                "(@Id, @FirstName, @LastName, @MiddleName, @DateOfBirth, @OfficeId, @AccountId, @Address)";

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
                { "@AccountId", receptionist.AccountId },
                { "@Address", receptionist.Address }
            };

            var parameters = new DynamicParameters(dict);

            string query = $"UPDATE {GetTableName(_entityType)} " +
                "SET FirstName = @FirstName, LastName = @LastName, MiddleName = @MiddleName, DateOfBirth = @DateOfBirth, OfficeId = @OfficeId, AccountId = @AccountId, Address = @Address " +
                "WHERE Id = @Id";

            await Update(query, parameters);
        }

        public async Task UpdateReceptionistsOffice(IEnumerable<Guid> id, string address)
        {
            string query = "";

            foreach (var receprionistsId in id)
            {
                query += $"UPDATE {GetTableName(_entityType)} " +
                $"SET Address = '{address}' " +
                $"WHERE Id = '{receprionistsId}' ";
            }

            await Update(query);
        }
    }
}
