using System.Threading.Tasks;
using ClientService.Core.Entities;
using MassTransit;
using RentService.Core.Interfaces;

namespace RentService.Queues
{
    public class ClientConsumer : IConsumer<Client>
    {
        private readonly IRentRepository _rentRepository;

        public ClientConsumer(IRentRepository rentRepository)
        {
            _rentRepository = rentRepository;
        }

        public async Task Consume(ConsumeContext<Client> context)
        {
            await _rentRepository.UpdateByClient(context.Message.ID, context.Message.Name);
        }
    }
}
