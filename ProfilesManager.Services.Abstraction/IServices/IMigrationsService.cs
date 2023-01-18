namespace ProfilesManager.Services.Abstraction.IServices
{
    public interface IMigrationsService
    {
        void CreateTables();
        void DeleteTables();
        void ClearTables();
    }
}
