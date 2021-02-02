using System;
using System.Threading.Tasks;
using CarService.Core.Entities;
using MassTransit;
using RentService.Core.Interfaces;

namespace RentService.Queues
{
    public class CarConsumer : IConsumer<Car>
    {
        private readonly IRentRepository _rentRepository;

        public CarConsumer(IRentRepository rentRepository)
        {
            _rentRepository = rentRepository;
        }

        public async Task Consume(ConsumeContext<Car> context)
        {
            await _rentRepository.UpdateByCar(context.Message.ID, context.Message.Description);
        }
    }
}