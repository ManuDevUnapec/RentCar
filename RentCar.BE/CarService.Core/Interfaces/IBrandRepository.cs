using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarService.Core.Entities;

namespace CarService.Core.Interfaces
{
    public interface IBrandRepository : IGenericRepository<Brand>
    {
        Task<IEnumerable<Brand>> GetReport(int? id, string description, string status);
    }
}
