using InnoClinic.Domain.Messages.Office;
using MassTransit;
using ProfilesManager.Services.Abstraction.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfilesManager.Messaging.Consumers
{
    public class OfficeDeletedConsumer : IConsumer<OfficeDeleted>
    {
        private readonly IServiceManager _serviceManager;

        public OfficeDeletedConsumer(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public async Task Consume(ConsumeContext<OfficeDeleted> context)
        {
            var message = context.Message;

            _serviceManager.DoctorsService.UpdateDoctorsAddress(message.Id, "undefined");
            _serviceManager.ReceptionistsService.UpdateReceptionistsAddress(message.Id, "undefined");
        }
    }
}
