using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeService.Core.Entities;

namespace EmployeeService.Core.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        
    }
}