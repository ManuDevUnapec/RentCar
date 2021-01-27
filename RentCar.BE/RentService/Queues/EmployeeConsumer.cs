using System;
using System.Threading.Tasks;
using EmployeeService.Core.Entities;
using MassTransit;
using RentService.Core.Interfaces;

namespace RentService.Queues
{
    public class EmployeeConsumer : IConsumer<Employee>
    {
        private readonly IRentRepository _rentRepository;

        public EmployeeConsumer(IRentRepository rentRepository)
        {
            _rentRepository = rentRepository;
        }

        public async Task Consume(ConsumeContext<Employee> context)
        {
            await _rentRepository.UpdateByEmployee(context.Message.ID, context.Message.Name);
        }
    }
}
