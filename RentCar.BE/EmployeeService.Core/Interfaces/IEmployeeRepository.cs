using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeService.Core.Entities;

namespace EmployeeService.Core.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetReport(int? id, string name, string identificationCard,
            string hourHand, string commisionPercent, DateTime? created, string status);
    }
}