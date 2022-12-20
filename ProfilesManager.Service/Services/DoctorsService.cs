using AutoMapper;
using ProfilesManager.Contracts.Models;
using ProfilesManager.Domain.Entities;
using ProfilesManager.Domain.IRepositories;
using ProfilesManager.Domain.MyExceptions;
using ProfilesManager.Services.Abstraction.IServices;

namespace ProfilesManager.Service.Services
{
    public class DoctorsService : IDoctorsService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public DoctorsService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Doctor>> GetDoctors()
        {
            var doctorEntity = await _repositoryManager.DoctorsRepository.GetAll();

            return _mapper.Map<IEnumerable<Doctor>>(doctorEntity);
        }

        public async Task<IEnumerable<DoctorForPatient>> GetDoctorsForPatient()
        {
            var doctorEntity = await _repositoryManager.DoctorsRepository.GetAll();

            return _mapper.Map<IEnumerable<DoctorForPatient>>(doctorEntity);
        }

        public async Task<Doctor> GetDoctorById(Guid id)
        {
            var doctor = await _repositoryManager.DoctorsRepository.GetById(id);

            if (doctor == null)
            {
                throw new NotFoundException("Doctor with entered Id does not exsist");
            }

            return _mapper.Map<Doctor>(doctor);
        }

        public async Task<Doctor> GetDoctorByName(string name)
        {
            var doctor = await _repositoryManager.DoctorsRepository.GetByName(name);

            if (doctor == null)
            {
                throw new NotFoundException("Doctor with entered Id does not exsist");
            }

            return _mapper.Map<Doctor>(doctor);
        }

        public async Task<DoctorForPatient> GetDoctorByIdForPatient(Guid id)
        {
            var doctor = await _repositoryManager.DoctorsRepository.GetById(id);

            if (doctor == null)
            {
                throw new NotFoundException("Doctor with entered Id does not exsist");
            }

            return _mapper.Map<DoctorForPatient>(doctor);
        }

        public async Task<Doctor> GetDoctorByOffice(Guid officeId)
        {
            var doctor = await _repositoryManager.DoctorsRepository.GetByOffice(officeId);

            if (doctor == null)
            {
                throw new NotFoundException("Doctor with entered Id does not exsist");
            }

            return _mapper.Map<Doctor>(doctor);
        }

        public async Task<Doctor> GetDoctorBySpecialization(string specializationName)
        {
            var doctor = await _repositoryManager.DoctorsRepository.GetBySpecialization(specializationName);

            if (doctor == null)
            {
                throw new NotFoundException("Doctor with entered Id does not exsist");
            }

            return _mapper.Map<Doctor>(doctor);
        }

        public async Task<Doctor> CreateDoctor(Doctor doctor)
        {
            var doctorEntity = _mapper.Map<DoctorEntity>(doctor);
            doctorEntity.Id = Guid.NewGuid();

            _repositoryManager.DoctorsRepository.Create(doctorEntity);

            return _mapper.Map<Doctor>(doctorEntity);
        }

        public async Task UpdateDoctor(Guid id, Doctor doctor)
        {
            var doctorEntity = await _repositoryManager.DoctorsRepository.GetById(id);
            doctor.Id = id;

            if (doctorEntity == null)
            {
                throw new NotFoundException("Doctor with entered Id does not exsist");
            }

            _mapper.Map(doctor, doctorEntity);

            await _repositoryManager.DoctorsRepository.Update(doctorEntity);
        }

        public async Task UpdateDoctorStatus(Guid id, int doctorStatus)
        {
            var doctorEntity = await _repositoryManager.DoctorsRepository.GetById(id);

            if (doctorEntity == null)
            {
                throw new NotFoundException("Doctor with entered Id does not exsist");
            }

            await _repositoryManager.DoctorsRepository.UpdateDoctorStatus(id, doctorStatus);
        }

        public async Task DeleteDoctor(Guid id)
        {
            var doctorEntity = await _repositoryManager.DoctorsRepository.GetById(id);

            if (doctorEntity == null)
            {
                throw new NotFoundException("Doctor with entered Id does not exsist");
            }

            await _repositoryManager.DoctorsRepository.Delete(id);
        }
    }
}
