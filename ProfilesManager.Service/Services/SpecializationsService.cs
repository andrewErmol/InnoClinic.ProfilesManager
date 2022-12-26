using AutoMapper;
using ProfilesManager.Contracts.Models;
using ProfilesManager.Domain.Entities;
using ProfilesManager.Domain.IRepositories;
using ProfilesManager.Domain.MyExceptions;
using ProfilesManager.Services.Abstraction.IServices;

namespace ProfilesManager.Service.Services
{
    public class SpecializationsService : ISpecializationsService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public SpecializationsService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Specialization>> GetSpecializations()
        {
            var specializationEntity = await _repositoryManager.SpecializationsRepository.GetAll();

            return _mapper.Map<IEnumerable<Specialization>>(specializationEntity);
        }

        public async Task<Specialization> GetSpecializationById(Guid id)
        {
            var specialization = await _repositoryManager.SpecializationsRepository.GetById(id);

            if (specialization == null)
            {
                throw new NotFoundException("Specialization with entered Id does not exsist");
            }

            return _mapper.Map<Specialization>(specialization);
        }

        public async Task<Guid> CreateSpecialization(Specialization specialization)
        {
            var specializationEntity = _mapper.Map<SpecializationEntity>(specialization);
            specializationEntity.Id = Guid.NewGuid();

            _repositoryManager.SpecializationsRepository.Create(specializationEntity);

            return specializationEntity.Id;
        }

        public async Task UpdateSpecialization(Guid id, Specialization specialization)
        {
            var specializationEntity = await _repositoryManager.SpecializationsRepository.GetById(id);
            specialization.Id = id;
            if (specializationEntity == null)
            {
                throw new NotFoundException("Specialization with entered Id does not exsist");
            }

            _mapper.Map(specialization, specializationEntity);

            await _repositoryManager.SpecializationsRepository.Update(specializationEntity);
        }

        public async Task DeleteSpecialization(Guid id)
        {
            var specializationEntity = await _repositoryManager.SpecializationsRepository.GetById(id);

            if (specializationEntity == null)
            {
                throw new NotFoundException("Specialization with entered Id does not exsist");
            }

            await _repositoryManager.SpecializationsRepository.Delete(id);
        }
    }
}
