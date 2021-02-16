using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InspectionService.Core.Entities;

namespace InspectionService.Core.Interfaces
{
    public interface IInspectionRepository : IGenericInterface<Inspection>
    {
        Task<int> UpdateByClient(int clientID, string clientName);
        Task<int> UpdateByEmployee(int employeeID, string employeeName);
        Task<int> UpdateByCar(int carID, string carName);
        Task<IEnumerable<Inspection>> GetReport(int? id, bool? grated,
            bool? cat, bool? rubberBack, bool? glassBreak, bool? rubberStateOne, bool? rubberStateTwo,
            bool? rubberStateThree, bool? rubberStateFour, string amountOfFuel, string status,
            int? employeeid, int? clientID, int? carID);
    }
}