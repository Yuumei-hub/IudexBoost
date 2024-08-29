using IudexBoost.Interface;
using IudexBoost.Models.Classes;
using Microsoft.EntityFrameworkCore;

namespace IudexBoost.Repository
{
    public abstract class GenericRepository<T>: IGenericRepository<T> where T : class
    {
        protected readonly Context _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(Context context)
        {
                _context = context;
            _dbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }
        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }
        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }
    }
}
