using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarService.Core.Entities;

namespace CarService.Core.Interfaces
{
    public interface ICarRepository : IGenericRepository<Car>
    {
        Task<IEnumerable<Car>> GetReport(int? id, string description, string status,
            int? brandID, int? modelID, int? typeOfCarID, int? typeOfModelID,
            string plateNumber, string engineNumber, string chassisNumber);
    }
}
