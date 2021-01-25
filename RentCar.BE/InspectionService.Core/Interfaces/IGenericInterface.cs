using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InspectionService.Core.Interfaces
{
    public interface IGenericInterface<T> where T : class
    {
        Task<T> Get(int id);
        Task<IEnumerable<T>> GetAll();
        Task<int> Add(T entity);
        Task<int> Delete(int id);
        Task<int> Update(T entity);
    }
}