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
            if (parameters.OfficeAddress != null)
            {
                query += $" AND Doctors.Address = '{parameters.OfficeAddress}'";
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


            if (!string.IsNullOrWhiteSpace(parameters.FirstName))
            {
                doctors = doctors.Where(d => d.FirstName.ToLower().Contains(parameters.FirstName.Trim().ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(parameters.LastName))
            {
                doctors = doctors.Where(d => d.LastName.ToLower().Contains(parameters.LastName.Trim().ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(parameters.MiddleName))
            {
                doctors = doctors.Where(d => d.MiddleName.ToLower().Contains(parameters.MiddleName.Trim().ToLower()));
            }

            return doctors;
        }

        public async Task<IEnumerable<Guid>> GetDoctorsIdsByOfficeId(Guid id)
        {
            var query = $"SELECT Id FROM {GetTableName(_entityType)} " +
                $"WHERE Doctors.OfficeId = '{id}'";

            IEnumerable<Guid> doctorsIds;

            using (IDbConnection db = new SqlConnection(connectionString))
            {
                doctorsIds = await db.QueryAsync<Guid>(query);
            }

            return doctorsIds;
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

        public async Task UpdateDoctorsOffice(IEnumerable<Guid> id, string address)
        {
            string query = "";

            foreach (var doctorsId in id)
            {
                query += $"UPDATE {GetTableName(_entityType)} " +
                $"SET Address = '{address}' " +
                $"WHERE Id = '{doctorsId}' ";
            }

            await Update(query);
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
