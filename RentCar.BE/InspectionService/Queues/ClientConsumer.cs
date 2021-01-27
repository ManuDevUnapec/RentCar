using System;
using System.Threading.Tasks;
using ClientService.Core.Entities;
using InspectionService.Core.Interfaces;
using MassTransit;

namespace InspectionService.Queues
{
    public class ClientConsumer : IConsumer<Client>
    {
        private readonly IInspectionRepository _inspectionRepository;

        public ClientConsumer(IInspectionRepository inspectionRepository)
        {
            _inspectionRepository = inspectionRepository;
        }

        public async Task Consume(ConsumeContext<Client> context)
        {
            await _inspectionRepository.UpdateByClient(context.Message.ID, context.Message.Name);
        }
    }
}