using System;
using System.Threading.Tasks;
using EmployeeService.Core.Entities;
using InspectionService.Core.Interfaces;
using MassTransit;

namespace InspectionService.Queues
{
    public class EmployeeConsumer : IConsumer<Employee>
    {
        private readonly IInspectionRepository _inspectionRepository;

        public EmployeeConsumer(IInspectionRepository inspectionRepository)
        {
            _inspectionRepository = inspectionRepository;
        }

        public async Task Consume(ConsumeContext<Employee> context)
        {
            await _inspectionRepository.UpdateByEmployee(context.Message.ID, context.Message.Name);
        }
    }
}
