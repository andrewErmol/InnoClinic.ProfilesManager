using Dapper;
using ProfilesManager.Domain.Entities;
using ProfilesManager.Domain.IRepositories;

namespace ProfilesManager.Persistence.Repositories
{
    public class PatientsRepository : RepositoryBase<PatientEntity>, IPatientsRepository
    {
        private Type _entityType = typeof(PatientEntity);

        public PatientsRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<PatientEntity>> GetAll() => 
            await FindAll(_entityType);

        public async Task<PatientEntity> GetById(Guid id)
        {
            var patientEntity = await FindByCondition(
                $"SELECT * FROM {GetTableName(_entityType)} WHERE Id = '{id}'"
                );

            return patientEntity.SingleOrDefault();
        }

        public async Task Create(PatientEntity patient)
        {
            var dict = new Dictionary<string, object>
            {
                { "@Id", patient.Id },
                { "@FirstName", patient.FirstName },
                { "@LastName", patient.LastName },
                { "@MiddleName", patient.MiddleName },
                { "@DateOfBirth", patient.DateOfBirth },
                { "@AccountId", patient.AccountId }
            };

            var parameters = new DynamicParameters(dict);

            string query = $"INSERT INTO {GetTableName(patient.GetType())} VALUES" +
                "(@Id, @FirstName, @LastName, @MiddleName, @DateOfBirth, @AccountId)";

            await Create(query, parameters);
        }

        public async Task Delete(Guid id) => await Delete(_entityType, id);

        public async Task Update(PatientEntity patient)
        {
            var dict = new Dictionary<string, object>
            {
                { "@Id", patient.Id },
                { "@FirstName", patient.FirstName },
                { "@LastName", patient.LastName },
                { "@MiddleName", patient.MiddleName },
                { "@DateOfBirth", patient.DateOfBirth },
                { "@AccountId", patient.AccountId }
            };

            var parameters = new DynamicParameters(dict);

            string query = $"UPDATE {GetTableName(_entityType)} " +
                "SET FirstName = @FirstName, LastName = @LastName, MiddleName = @MiddleName, DateOfBirth = @DateOfBirth, AccountId = @AccountId " +
                "WHERE Id = @Id";

            await Update(query, parameters);
        }
    }
}
