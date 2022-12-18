using ProfilesManager.Domain.IRepositories;

namespace ProfilesManager.Persistence.DapperImplementation
{
    public class RepositoryManager : IRepositoryManager
    {
        private string _connectionString;

        private IDoctorsRepository _doctorsRepository;
        private IPatientsRepository _patientsRepository;
        private ISpecializationsRepository _specializationsRepository;
        private IReceptionistsRepository _receptionistsRepository;

        public RepositoryManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDoctorsRepository DoctorsRepository
        {
            get
            {
                if (_doctorsRepository == null)
                {
                    _doctorsRepository = new DoctorsRepository(_connectionString);
                }
                return _doctorsRepository;
            }
        }

        public IPatientsRepository PatientsRepository
        {
            get
            {
                if (_patientsRepository == null)
                {
                    _patientsRepository = new PatientsRepository(_connectionString);
                }
                return _patientsRepository;
            }
        }

        public ISpecializationsRepository SpecializationsRepository
        {
            get
            {
                if (_specializationsRepository == null)
                {
                    _specializationsRepository = new SpecializationsRepository(_connectionString);
                }
                return _specializationsRepository;
            }
        }

        public IReceptionistsRepository ReceptionistsRepository
        {
            get
            {
                if (_receptionistsRepository == null)
                {
                    _receptionistsRepository = new ReceptionistsRepository(_connectionString);
                }
                return _receptionistsRepository;
            }
        }
    }
}
