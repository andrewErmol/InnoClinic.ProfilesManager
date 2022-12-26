using AutoMapper;
using ProfilesManager.Contracts.Models;
using ProfilesManager.Domain.Entities;
using ProfilesManager.Domain.IRepositories;
using ProfilesManager.Domain.MyExceptions;
using ProfilesManager.Services.Abstraction.IServices;

namespace ProfilesManager.Service.Services
{
    public class ReceptionistsService : IReceptionistsService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public ReceptionistsService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Receptionist>> GetReceptionists()
        {
            var receptionistEntity = await _repositoryManager.ReceptionistsRepository.GetAll();

            return _mapper.Map<IEnumerable<Receptionist>>(receptionistEntity);
        }

        public async Task<Receptionist> GetReceptionistById(Guid id)
        {
            var receptionist = await _repositoryManager.ReceptionistsRepository.GetById(id);

            if (receptionist == null)
            {
                throw new NotFoundException("Receptionist with entered Id does not exsist");
            }

            return _mapper.Map<Receptionist>(receptionist);
        }

        public async Task<Guid> CreateReceptionist(Receptionist receptionist)
        {
            var receptionistEntity = _mapper.Map<ReceptionistEntity>(receptionist);
            receptionistEntity.Id = Guid.NewGuid();

            _repositoryManager.ReceptionistsRepository.Create(receptionistEntity);

            return receptionistEntity.Id;
        }

        public async Task UpdateReceptionist(Guid id, Receptionist receptionist)
        {
            var receptionistEntity = await _repositoryManager.ReceptionistsRepository.GetById(id);
            receptionist.Id = id;

            if (receptionistEntity == null)
            {
                throw new NotFoundException("Receptionist with entered Id does not exsist");
            }

            _mapper.Map(receptionist, receptionistEntity);

            await _repositoryManager.ReceptionistsRepository.Update(receptionistEntity);
        }

        public async Task DeleteReceptionist(Guid id)
        {
            var receptionistEntity = await _repositoryManager.ReceptionistsRepository.GetById(id);

            if (receptionistEntity == null)
            {
                throw new NotFoundException("Receptionist with entered Id does not exsist");
            }

            await _repositoryManager.ReceptionistsRepository.Delete(id);
        }
    }
}
