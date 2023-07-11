﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrganikHaberlesme.Application.Contracts.Persistence
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<T> Get(int id);

        public Task<IReadOnlyList<T>> GetAll();

        Task<bool> Exists(int id);

        public Task<T> Add(T entity);

        public Task Update(T entity);

        public Task Delete(T entity);
    }
}
