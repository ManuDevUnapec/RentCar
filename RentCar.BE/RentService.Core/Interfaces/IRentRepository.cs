using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RentService.Core.Entities;

namespace RentService.Core.Interfaces
{
    public interface IRentRepository : IGenericRepository<Rent>
    {
        Task<int> UpdateByClient(int clientID, string clientName);
        Task<int> UpdateByEmployee(int employeeID, string employeeName);
        Task<int> UpdateByCar(int carID, string carName);
        Task<IEnumerable<Rent>> GetReport(int? id,int? amountForDays, int? numberOfDays,
            string status, int? employeeID, int? clientID, int? carID);
    }
}
