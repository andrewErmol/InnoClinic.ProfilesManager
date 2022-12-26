using ProfilesManager.Domain.Entities;
using ProfilesManager.Persistence.IDapperImplementation;
using ProfilesManager.Services.Abstraction.IServices;

namespace ProfilesManager.Service.Services
{
    public class MigrationsSevice : IMigrationsService
    {
        private readonly ITablesManager _tablesManager;

        public MigrationsSevice(ITablesManager tablesManager)
        {
            _tablesManager = tablesManager;
        }

        public void CreateTables()
        {
            Dictionary<Type, Type[]> modelAndNavPropsTypes = new Dictionary<Type, Type[]>
            {
                { typeof(PatientEntity), null },
                { typeof(ReceptionistEntity), null },
                { typeof(SpecializationEntity), null },
                { typeof(DoctorEntity), new Type[] { typeof(SpecializationEntity) } }
            };

            _tablesManager.CreateTable(modelAndNavPropsTypes);
        }

        public void DeleteTables()
        {
            _tablesManager.DeleteTables(
                typeof(DoctorEntity),
                typeof(PatientEntity),
                typeof(ReceptionistEntity),
                typeof(SpecializationEntity)
                );
        }

        public void ClearTables()
        {
            _tablesManager.ClearTables(
                typeof(DoctorEntity),
                typeof(PatientEntity),
                typeof(ReceptionistEntity),
                typeof(SpecializationEntity)
                );
        }
    }
}
