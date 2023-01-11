using InnoClinic.Domain.Messages;
using InnoClinic.Domain.Messages.Office;
using MassTransit;
using ProfilesManager.Service.Services;
using ProfilesManager.Services.Abstraction.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfilesManager.Messaging.Consumers
{
    public class OfficeUpdatedConsumer : IConsumer<OfficeUpdated>
    {
        private readonly IServiceManager _serviceManager;

        public OfficeUpdatedConsumer(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public async Task Consume(ConsumeContext<OfficeUpdated> context)
        {
            var message = context.Message;
            
            _serviceManager.DoctorsService.UpdateDoctorsAddress(message.Id, message.Address);
            _serviceManager.ReceptionistsService.UpdateReceptionistsAddress(message.Id, message.Address);
        }
    }
}
