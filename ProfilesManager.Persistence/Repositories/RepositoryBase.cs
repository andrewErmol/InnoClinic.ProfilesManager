using Dapper;
using ProfilesManager.Domain.IRepositories;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;

namespace ProfilesManager.Persistence.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly string connectionString;

        public RepositoryBase(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<IEnumerable<T>> FindAll(Type entityType)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return await db.QueryAsync<T>($"SELECT * FROM {GetTableName(entityType)}");
            }
        }

        public async Task<IEnumerable<T>> FindByCondition(string query)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return await db.QueryAsync<T>(query);
            }
        }

        public async Task Create(string query, DynamicParameters parameters)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                await db.ExecuteAsync(query, parameters);
            }
        }

        public async Task Update(string query, DynamicParameters parameters)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                await db.ExecuteAsync(query, parameters);
            }
        }

        public async Task Update(string query)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                await db.ExecuteAsync(query);
            }
        }

        public async Task Delete(Type entityType, Guid id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"DELETE FROM {GetTableName(entityType)} WHERE Id = @id";
                await db.ExecuteAsync(sqlQuery, new { id });
            }
        }

        protected string GetTableName(Type type)
        {
            object[] attributes = type.GetCustomAttributes(false);

            foreach (Attribute attr in attributes)
            {
                if (attr is TableAttribute tableAttribute)
                    return tableAttribute.Name;
            }

            return type.Name;
        }
    }
}
