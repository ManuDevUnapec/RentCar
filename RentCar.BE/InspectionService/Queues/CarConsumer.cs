using System;
using System.Threading.Tasks;
using CarService.Core.Entities;
using InspectionService.Core.Interfaces;
using MassTransit;

namespace InspectionService.Queues
{
    public class CarConsumer : IConsumer<Car>
    {
        private readonly IInspectionRepository _inspectionRepository;

        public CarConsumer(IInspectionRepository inspectionRepository)
        {
            _inspectionRepository = inspectionRepository;
        }

        public async Task Consume(ConsumeContext<Car> context)
        {
            await _inspectionRepository.UpdateByCar(context.Message.ID, context.Message.Description);
        }
    }
}