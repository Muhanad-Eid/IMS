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
        public List<T> Get(string name)
        {
            var data = _dbSet.ToList();
            return data;   
        }
    }
}
