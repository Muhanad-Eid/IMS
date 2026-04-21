using Application.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Repositories
{
    public class GenericRepository <T>:IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public  IQueryable<T> GetAll()
        {
            var data = _dbSet.AsQueryable();
            return data;   
        }
        public T GetById(int id)
        {
            var user = _dbSet.Find(id);
            return user;
        }
        public async Task<T> GetByIdAsync(int id)
        {
            var user = await _dbSet.FindAsync(id);
            return user;
        }
        public void Create(T item)
        {
             _dbSet.Add(item);
        }
        public async Task CreateAsync(T item)
        {
              await _dbSet.AddAsync(item);
        }
        public void Delete(T item)
        {
            _dbSet.Remove(item);
        }

        public void Update(T item)
        {
            _dbSet.Update(item);
        }

        public void CreateRange(IEnumerable<T> items)
        {
            _dbSet.AddRange(items);
        }

        public async Task CreateRangeAsync(IEnumerable<T> items)
        {
            await _dbSet.AddRangeAsync(items);
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
