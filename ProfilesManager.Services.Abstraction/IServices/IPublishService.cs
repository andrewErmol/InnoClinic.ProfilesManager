using ProfilesManager.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfilesManager.Services.Abstraction.IServices
{
    public interface IPublishService
    {
        Task PublishDoctorUpdatedMessage(Doctor doctor);
        Task PublishPatientUpdatedMessage(Patient patient);
    }
}
