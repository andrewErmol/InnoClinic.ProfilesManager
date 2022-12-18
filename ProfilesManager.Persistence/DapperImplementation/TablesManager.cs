using Dapper;
using ProfilesManager.Domain.IDapperImplementation;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace ProfilesManager.Persistence.DapperImplementation
{
    public class TablesManager : ITablesManager
    {
        private readonly string _connectionString;

        private readonly Type[] _baseFieldTypes = new Type[]
        {
            typeof(string),
            typeof(int),
            typeof(double),
            typeof(decimal),
            typeof(DateTime),
            typeof(bool),
            typeof(Guid)
        };

        public TablesManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void CreateTable(Dictionary<Type, Type[]> modelAndNavPropsTypes)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                foreach (var pair in modelAndNavPropsTypes)
                {
                    if (!IsTableExists(GetTableName(pair.Key)))
                    {
                        StringBuilder sqlQuery = new StringBuilder();

                        sqlQuery.Append($"CREATE TABLE {GetTableName(pair.Key)} (Id UNIQUEIDENTIFIER PRIMARY KEY, ");

                        foreach (var columnsPair in GetColumns(pair.Key, pair.Value))
                        {
                            sqlQuery.AppendLine($"{columnsPair.Key} {columnsPair.Value}, ");
                        }

                        sqlQuery.Append(")");

                        db.Execute(sqlQuery.ToString());
                    }
                }
            }
        }

        public void DeleteTables(params Type[] modelTypes)
        {
            foreach (Type type in modelTypes)
            {
                if (IsTableExists(GetTableName(type)))
                {
                    using (IDbConnection db = new SqlConnection(_connectionString))
                    {
                        db.Execute($"DROP TABLE {GetTableName(type)}");
                    }
                }
            }
        }

        private bool IsTableExists(string tableName)
        {
            string query = $"SELECT * FROM {tableName}";
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    db.Execute(query);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private string GetTableName(Type type)
        {
            object[] attributes = type.GetCustomAttributes(false);

            foreach (Attribute attr in attributes)
            {
                if (attr is TableAttribute tableAttribute)
                    return tableAttribute.Name;
            }

            return type.Name;
        }

        private Dictionary<string, string> GetColumns(Type modelType, Type[] navPropsTypes)
        {
            Dictionary<string, string> columns = new Dictionary<string, string>();
            var properties = modelType.GetProperties();

            var allProps = from prop in properties
                           where _baseFieldTypes.Contains(prop.PropertyType) && prop.Name != "Id"
                           select prop;

            string value = "";

            foreach (var p in allProps)
            {
                if (p.Name.Substring(p.Name.Length - 2) == "Id")
                {
                    if (navPropsTypes is not null)
                    {
                        if (navPropsTypes.FirstOrDefault(f => f.Name == p.Name.Substring(0, p.Name.Length - 2)) != null)
                        {
                            value = $"FOREIGN KEY {GetModelType(p.PropertyType.Name)} REFERENCES " +
                                $"{GetTableName(navPropsTypes.FirstOrDefault(f => f.Name == p.Name.Substring(0, p.Name.Length - 2)))} " +
                                $"(Id) ON DELETE CASCADE";
                        }
                        else
                        {
                            value = GetModelType(p.PropertyType.Name) + " NOT NULL";
                        }
                    }
                    else
                    {
                        value = GetModelType(p.PropertyType.Name) + " NOT NULL";
                    }
                }
                else
                {
                    value = GetModelType(p.PropertyType.Name) + " NOT NULL";
                }

                columns.Add(p.Name, value);
            }

            return columns;
        }

        public IEnumerable<PropertyInfo> GetBaseProps(Type type)
        {
            return from prop in type.GetProperties()
                   where _baseFieldTypes.Contains(prop.PropertyType) && prop.Name != "Id"
                   select prop;
        }

        private string GetModelType(string typeName)
        {
            switch (typeName)
            {
                case "DateTime":
                    return "DATETIME";
                case "String":
                    return "NVARCHAR(1000)";
                case "Int32":
                    return "INT";
                case "Boolean":
                    return "BIT";
                case "Decimal":
                    return "MONEY";
                case "Guid":
                    return "UNIQUEIDENTIFIER";
                case "ProcedureCategory":
                    return "INT";
                default:
                    throw new Exception("This data type is not processed");
            }
        }

        public void ClearTables(params Type[] modelTypes)
        {
            foreach (Type type in modelTypes)
            {
                string query = $"DELETE FROM {GetTableName(type)}";

                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    db.Execute(query);
                }
            }
        }
    }
}
