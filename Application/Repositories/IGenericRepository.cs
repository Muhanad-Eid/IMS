using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        void Create(T item);
        Task CreateAsync(T item);
        void CreateRange(IEnumerable<T> items);
        Task CreateRangeAsync(IEnumerable<T> items);
        void Update(T item);
        void Delete(T item); 
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
