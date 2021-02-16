using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarService.Core.Entities;

namespace CarService.Core.Interfaces
{
    public interface ITypeOfCarRepository : IGenericRepository<TypeOfCar>
    {
        Task<IEnumerable<TypeOfCar>> GetReport(int? id, string description, string status);
    }
}
