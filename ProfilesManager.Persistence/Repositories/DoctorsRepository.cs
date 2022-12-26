using Dapper;
using ProfilesManager.Domain.Entities;
using ProfilesManager.Domain.IRepositories;
using ProfilesManager.Domain.Parametrs;
using System.Data;
using System.Data.SqlClient;

namespace ProfilesManager.Persistence.DapperImplementation
{
    public class DoctorsRepository : RepositoryBase<DoctorEntity>, IDoctorsRepository
    {
        private Type _entityType = typeof(DoctorEntity);

        public DoctorsRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<DoctorEntity>> GetDoctors(ParametersForGetDoctors parameters)
        {          
            var query = $"SELECT * FROM {GetTableName(_entityType)} " +
                $"JOIN Specializations ON Specializations.Id = SpecializationId " +
                $"WHERE 1 = 1";

            if (parameters.Specialization != null)
            {
                query += $" AND Specializations.Name = '{parameters.Specialization}'";
            }
            if (parameters.OfficeId != null)
            {
                query += $" AND Doctors.OfficeId = '{Guid.Parse(parameters.OfficeId)}'";
            }
            if (parameters.FirstName != null)
            {
                query += $" AND Doctors.FirstName = '{parameters.FirstName}'";
            }
            if (parameters.LastName != null)
            {
                query += $" AND Doctors.LastName = '{parameters.LastName}'";
            }
            if (parameters.MiddleName != null)
            {
                query += $" AND Doctors.MiddleName = '{parameters.MiddleName}'";
            }

            IEnumerable<DoctorEntity> doctors;

            using (IDbConnection db = new SqlConnection(connectionString))
            {
                doctors = await db.QueryAsync<DoctorEntity, SpecializationEntity, DoctorEntity>(query, (doctor, specialization) =>
                {   
                    doctor.Specialization = specialization;
                    return doctor;
                });
            }

            return doctors;
        }

        public async Task<DoctorEntity> GetById(Guid id)
        {
            var query = $"SELECT * FROM {GetTableName(_entityType)} " +
                $"JOIN Specializations ON Specializations.Id = SpecializationId " +
                $"WHERE Doctors.Id = '{id}'";

            IEnumerable<DoctorEntity> doctors;

            using (IDbConnection db = new SqlConnection(connectionString))
            {
                doctors = await db.QueryAsync<DoctorEntity, SpecializationEntity, DoctorEntity>(query, (doctor, specialization) =>
                {
                    doctor.Specialization = specialization;
                    return doctor;
                });
            }

            return doctors.SingleOrDefault();
        }        

        public async Task Create(DoctorEntity doctor)
        {
            var dict = new Dictionary<string, object>
            {
                { "@Id", doctor.Id },
                { "@FirstName", doctor.FirstName },
                { "@LastName", doctor.LastName },
                { "@MiddleName", doctor.MiddleName },
                { "@DateOfBirth", doctor.DateOfBirth },
                { "@Status", (int)doctor.Status },
                { "@AccountId", doctor.AccountId },
                { "@SpecializationId", doctor.SpecializationId },
                { "@OfficeId", doctor.OfficeId },
                { "@CareerStartYear", doctor.CareerStartYear },
                { "@Address", doctor.Address },
            };
            
            var parameters = new DynamicParameters(dict);

            string query = $"INSERT INTO {GetTableName(doctor.GetType())} VALUES" +
                "(@Id, @FirstName, @LastName, @MiddleName, @DateOfBirth, @Status, @AccountId, @SpecializationId, @OfficeId, @CareerStartYear, @Address)";

            await Create(query, parameters);
        }

        public async Task Delete(Guid id) => await Delete(_entityType, id);

        public async Task Update(DoctorEntity doctor)
        {
            var dict = new Dictionary<string, object>
            {
                { "@Id", doctor.Id },
                { "@FirstName", doctor.FirstName },
                { "@LastName", doctor.LastName },
                { "@MiddleName", doctor.MiddleName },
                { "@DateOfBirth", doctor.DateOfBirth },
                { "@Status", (int)doctor.Status },
                { "@AccountId", doctor.AccountId },
                { "@SpecializationId", doctor.SpecializationId },
                { "@OfficeId", doctor.OfficeId },
                { "@CareerStartYear", doctor.CareerStartYear },
                { "@Address", doctor.Address }
            };

            var parameters = new DynamicParameters(dict);

            string query = $"UPDATE {GetTableName(_entityType)} " +
                "SET FirstName = @FirstName, LastName = @LastName, MiddleName = @MiddleName, " +
                "DateOfBirth = @DateOfBirth, Status = @Status, AccountId = @AccountId, SpecializationId = @SpecializationId, " +
                "OfficeId = @OfficeId, CareerStartYear = @CareerStartYear, Address = @Address " +
                "WHERE Id = @Id";

            await Update(query, parameters);
        }

        public async Task UpdateDoctorStatus(Guid id, DoctorStatus doctorStatus)
        {
            var dict = new Dictionary<string, object>
            {
                { "@Status", (int)doctorStatus }
            };

            var parameters = new DynamicParameters(dict);

            string query = $"UPDATE {GetTableName(_entityType)} " +
                "SET Status = @Status " +
                $"WHERE Id = '{id}'";

            await Update(query, parameters);
        }
    }
}
