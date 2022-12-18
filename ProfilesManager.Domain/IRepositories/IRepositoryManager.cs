namespace ProfilesManager.Domain.IRepositories
{
    public interface IRepositoryManager
    {
        IDoctorsRepository DoctorsRepository { get; }
        IPatientsRepository PatientsRepository { get; }
        IReceptionistsRepository ReceptionistsRepository { get; }
        ISpecializationsRepository SpecializationsRepository { get; }
    }
}
