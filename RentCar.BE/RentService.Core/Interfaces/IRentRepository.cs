﻿using System;
using System.Threading.Tasks;
using RentService.Core.Entities;

namespace RentService.Core.Interfaces
{
    public interface IRentRepository : IGenericRepository<Rent>
    {
        Task<int> UpdateByClient(int clientID, string clientName);
        Task<int> UpdateByEmployee(int employeeID, string employeeName);
        Task<int> UpdateByCar(int carID, string carName);
    }
}
