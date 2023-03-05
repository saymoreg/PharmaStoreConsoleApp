using System;
using Core.Entities;

namespace Data.Repositories.Abstract
{
    public interface IRepository<T> where T : BaseEntity
    {
        List<T> GetAll();
        void Create(T item);
        void Update(T item);
        void Delete(T item);
    }
}

