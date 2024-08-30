using IudexBoost.Models.Classes;
using Microsoft.EntityFrameworkCore;

namespace IudexBoost.Repository
{
    public abstract class GenericRepository<T> where T : class
    {
        protected readonly Context _context;
        protected readonly DbSet<T> _dbSet;
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
        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }
        public virtual T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }
    }
}
