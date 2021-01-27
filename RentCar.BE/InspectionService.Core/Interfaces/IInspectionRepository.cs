using System;
using System.Threading.Tasks;
using InspectionService.Core.Entities;

namespace InspectionService.Core.Interfaces
{
    public interface IInspectionRepository : IGenericInterface<Inspection>
    {
        Task<int> UpdateByClient(int clientID, string clientName);
    }
}
