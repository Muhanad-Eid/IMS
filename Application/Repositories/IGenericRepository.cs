using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        List<T> Get(string name);
    }
}
