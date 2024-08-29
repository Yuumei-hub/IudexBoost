using NuGet.Protocol.Core.Types;

namespace IudexBoost.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        void Add(T entity);
        T GetById(int id);
        void Update(T entity);
        void Delete(T entity);
    }
}
