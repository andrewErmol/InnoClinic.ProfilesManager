using AutoMapper;
using ProfilesManager.Contracts.Models;
using ProfilesManager.Domain.Entities;
using ProfilesManager.Domain.IRepositories;
using ProfilesManager.Domain.MyExceptions;
using ProfilesManager.Services.Abstraction.IServices;

namespace ProfilesManager.Service.Services
{
    public class PatientsService : IPatientsService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public PatientsService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Patient>> GetPatients()
        {
            var patientEntity = await _repositoryManager.PatientsRepository.GetAll();

            return _mapper.Map<IEnumerable<Patient>>(patientEntity);
        }

        public async Task<Patient> GetPatientById(Guid id)
        {
            var patient = await _repositoryManager.PatientsRepository.GetById(id);

            if (patient == null)
            {
                throw new NotFoundException("Patient with entered Id does not exsist");
            }

            return _mapper.Map<Patient>(patient);
        }

        public async Task<Guid> CreatePatient(Patient patient)
        {
            var patientEntity = _mapper.Map<PatientEntity>(patient);
            patientEntity.Id = Guid.NewGuid();

            await _repositoryManager.PatientsRepository.Create(patientEntity);

            return patientEntity.Id;
        }

        public async Task UpdatePatient(Guid id, Patient patient)
        {
            var patientEntity = await _repositoryManager.PatientsRepository.GetById(id);
            patient.Id = id;

            if (patientEntity == null)
            {
                throw new NotFoundException("Patient with entered Id does not exsist");
            }

            _mapper.Map(patient, patientEntity);

            await _repositoryManager.PatientsRepository.Update(patientEntity);
        }

        public async Task DeletePatient(Guid id)
        {
            var patientEntity = await _repositoryManager.PatientsRepository.GetById(id);

            if (patientEntity == null)
            {
                throw new NotFoundException("Patient with entered Id does not exsist");
            }

            await _repositoryManager.PatientsRepository.Delete(id);
        }
    }
}
