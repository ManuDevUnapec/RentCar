using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClientService.Core.Entities;

namespace ClientService.Core.Interfaces
{
    public interface IClientRepository : IGenericRepository<Client>
    {
        Task<IEnumerable<Client>> GetReport(int? id, string name, string identificationCard,
            string cardNumber, int? creditLimit, string personType, string status);
    }
}
