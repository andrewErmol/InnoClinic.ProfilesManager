using AutoMapper;
using ProfilesManager.Contracts.Models;
using ProfilesManager.Domain.Entities;
using ProfilesManager.Domain.IRepositories;
using ProfilesManager.Domain.MyExceptions;
using ProfilesManager.Domain.Parametrs;
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

        public async Task<IEnumerable<Doctor>> GetDoctors(ParametersForGetDoctors parameters)
        {
            var doctorEntity = await _repositoryManager.DoctorsRepository.GetDoctors(parameters);

            return _mapper.Map<IEnumerable<Doctor>>(doctorEntity);
        }

        public async Task<IEnumerable<DoctorForPatient>> GetDoctorsForPatient(ParametersForGetDoctors parameters)
        {
            var doctorEntity = await _repositoryManager.DoctorsRepository.GetDoctors(parameters);

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

        public async Task<DoctorForPatient> GetDoctorByIdForPatient(Guid id)
        {
            var doctor = await _repositoryManager.DoctorsRepository.GetById(id);

            if (doctor == null)
            {
                throw new NotFoundException("Doctor with entered Id does not exsist");
            }

            return _mapper.Map<DoctorForPatient>(doctor);
        }

        public async Task<Guid> CreateDoctor(Doctor doctor)
        {
            DoctorStatus status;
            if (!Enum.TryParse(doctor.Status, out status))
            {
                throw new BadRequestException("Entered status dos not exist");
            }

            var specialization = await _repositoryManager.SpecializationsRepository.GetById(doctor.SpecializationId);
            if (specialization == null)
            {
                throw new BadRequestException("Entered specialization does not exist");
            }

            var doctorEntity = _mapper.Map<DoctorEntity>(doctor);
            doctorEntity.Id = Guid.NewGuid();

            await _repositoryManager.DoctorsRepository.Create(doctorEntity);

            return doctorEntity.Id;
        }

        public async Task UpdateDoctor(Guid id, Doctor doctor)
        {
            var doctorEntity = await _repositoryManager.DoctorsRepository.GetById(id);
            doctor.Id = id;

            if (doctorEntity == null)
            {
                throw new NotFoundException("Doctor with entered Id does not exsist");
            }

            var specialization = await _repositoryManager.SpecializationsRepository.GetById(doctor.SpecializationId);

            if (specialization == null)
            {
                throw new BadRequestException("Entered specialization does not exist");
            }

            DoctorStatus status;

            if (!Enum.TryParse(doctor.Status, out status))
            {
                throw new BadRequestException("Entered status does not exist");
            }

            _mapper.Map(doctor, doctorEntity);

            await _repositoryManager.DoctorsRepository.Update(doctorEntity);
        }

        public async Task UpdateDoctorsAddress(Guid id, string address)
        {
            var doctorsIds = await _repositoryManager.DoctorsRepository.GetDoctorsIdsByOfficeId(id);

            await _repositoryManager.DoctorsRepository.UpdateDoctorsOffice(doctorsIds, address);
        }

        public async Task UpdateDoctorStatus(Guid id, string doctorStatus)
        {
            var doctorEntity = await _repositoryManager.DoctorsRepository.GetById(id);

            if (doctorEntity == null)
            {
                throw new NotFoundException("Doctor with entered Id does not exsist");
            }

            DoctorStatus status;

            if (!Enum.TryParse(doctorStatus, out status))
            {
                throw new BadRequestException("Entered status dos not exist");
            }

            await _repositoryManager.DoctorsRepository.UpdateDoctorStatus(id, status);
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
