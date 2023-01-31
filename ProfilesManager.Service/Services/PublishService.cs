using InnoClinic.Domain.Messages.Profiles;
using MassTransit;
using ProfilesManager.Contracts.Models;
using ProfilesManager.Services.Abstraction.IServices;

namespace ProfilesManager.Services.Services
{
    public class PublishService : IPublishService
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public PublishService(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task PublishDoctorUpdatedMessage(Doctor doctor)
        {
            await _publishEndpoint.Publish(new DoctorUpdated
            {
                Id = doctor.Id,
                Name = $"{doctor.LastName} {doctor.FirstName} {doctor.MiddleName}"
            });
        }

        public async Task PublishPatientUpdatedMessage(Patient patient)
        {
            await _publishEndpoint.Publish(new PatientUpdated
            {
                Id = patient.Id,
                Name = $"{patient.LastName} {patient.FirstName} {patient.MiddleName}"
            });
        }
    }
}


