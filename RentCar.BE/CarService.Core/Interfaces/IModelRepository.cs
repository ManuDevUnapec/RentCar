using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarService.Core.Entities;

namespace CarService.Core.Interfaces
{
    public interface IModelRepository : IGenericRepository<Model>
    {
        Task<IEnumerable<Model>> GetReport(int? id, string description, string status, int? brandID);
    }
}
