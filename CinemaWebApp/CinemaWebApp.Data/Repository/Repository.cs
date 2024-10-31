using CinemaWebApp.Data.Repository.Contracts;
using CinemaWebApp.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace CinemaWebApp.Data.Repository
{
    public class Repository<TType, TId> : IRepository<TType, TId>
        where TType : class, new()
    {
        private readonly AppDbContext _context;
        private readonly DbSet<TType> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TType>();
        }

        public TType GetById(TId id)
        {
            TType entity = _dbSet.Find(id);
            return entity;
        }

        public async Task<TType> GetByIdAsync(TId id)
        {
            TType entity = await _dbSet.FindAsync(id);
            return entity;
        }

        public IEnumerable<TType> GetAll()
        {
            return _dbSet.ToArray();
        }

        public IEnumerable<TType> GetAllAttached()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<IEnumerable<TType>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public void Add(TType item)
        {
            throw new System.NotImplementedException();
        }

        public Task AddAsync(TType item)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(TId id)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteAsync(TId id)
        {
            throw new System.NotImplementedException();
        }

        public bool SoftDelete(TId id)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> SoftDeleteAsync(TId id)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(TType item)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateAsync(TType item)
        {
            throw new System.NotImplementedException();
        }
    }
}
