namespace ProfilesManager.Services.Abstraction.IServices
{
    public interface IServiceManager
    {
        IDoctorsService DoctorsService { get; }
        IPatientsService PatientsService { get; }
        IReceptionistsService ReceptionistsService { get; }
        ISpecializationsService SpecializationsService { get; }
    }
}
