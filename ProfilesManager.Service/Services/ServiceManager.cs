﻿using AutoMapper;
using ProfilesManager.Domain.IRepositories;
using ProfilesManager.Services.Abstraction.IServices;

namespace ProfilesManager.Service.Services
{
    public class ServiceManager : IServiceManager
    {
        private IRepositoryManager _repositoryManager;
        private IMapper _mapper;

        private IDoctorsService _doctorsService;
        private IPatientsService _patientsService;
        private IReceptionistsService _receptionistsService;
        private ISpecializationsService _specializationsService;

        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;

        }

        public IDoctorsService DoctorsService
        {
            get
            {
                if (_doctorsService == null)
                    _doctorsService= new DoctorsService(_repositoryManager, _mapper);
                return _doctorsService;
            }
        }

        public IPatientsService PatientsService
        {
            get
            {
                if (_patientsService == null)
                    _patientsService = new PatientsService(_repositoryManager, _mapper);
                return _patientsService;
            }
        }

        public IReceptionistsService ReceptionistsService
        {
            get
            {
                if (_receptionistsService == null)
                    _receptionistsService = new ReceptionistsService(_repositoryManager, _mapper);
                return _receptionistsService;
            }
        }

        public ISpecializationsService SpecializationsService
        {
            get
            {
                if (_specializationsService == null)
                    _specializationsService = new SpecializationsService(_repositoryManager, _mapper);
                return _specializationsService;
            }
        }
    }
}
