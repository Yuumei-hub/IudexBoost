﻿using IudexBoost.Repository;
using NuGet.Protocol.Core.Types;

namespace IudexBoost.Business.Interfaces
{
    public interface IGenericService<T> where T : class
    {
        void Add(T entity);
        T GetById(int id);
        void Update(T entity);
        void Delete(T entity);
    }
}
