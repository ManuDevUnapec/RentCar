using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarService.Core.Entities;

namespace CarService.Core.Interfaces
{
    public interface ITypeOfFuelRepository : IGenericRepository<TypeOfFuel>
    {
        Task<IEnumerable<TypeOfFuel>> GetReport(int? id, string description, string status);
    }
}
