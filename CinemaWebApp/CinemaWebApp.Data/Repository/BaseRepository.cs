using CinemaWebApp.Data.Repository.Contracts;
using CinemaWebApp.Models.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CinemaWebApp.Data.Repository
{
    public class BaseRepository<TType, TId> : IRepository<TType, TId>
        where TType : class, new()
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<TType> _dbSet;

        public BaseRepository(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
            this._dbSet = this._dbContext.Set<TType>();
        }

        public TType GetById(TId id)
        {
            TType entity = this._dbSet
                .Find(id);

            return entity;
        }

        public async Task<TType> GetByIdAsync(TId id)
        {
            TType entity = await this._dbSet
                .FindAsync(id);

            return entity;
        }

        public TType FirstOrDefault(Func<TType, bool> predicate)
        {
            TType entity = this._dbSet
                .FirstOrDefault(predicate);

            return entity;
        }

        public async Task<TType> FirstOrDefaultAsync(Expression<Func<TType, bool>> predicate)
        {
            TType entity = await this._dbSet
                .FirstOrDefaultAsync(predicate);

            return entity;
        }

        public IEnumerable<TType> GetAll()
        {
            return this._dbSet.ToArray();
        }

        public async Task<IEnumerable<TType>> GetAllAsync()
        {
            return await this._dbSet.ToArrayAsync();
        }

        public IQueryable<TType> GetAllAttached()
        {
            return this._dbSet.AsQueryable();
        }

        public void Add(TType item)
        {
            this._dbSet.Add(item);
            this._dbContext.SaveChanges();
        }

        public async Task AddAsync(TType item)
        {
            await this._dbSet.AddAsync(item);
            await this._dbContext.SaveChangesAsync();
        }

        public void AddRange(TType[] items)
        {
            this._dbSet.AddRange(items);
            this._dbContext.SaveChanges();
        }

        public async Task AddRangeAsync(TType[] items)
        {
            await this._dbSet.AddRangeAsync(items);
            await this._dbContext.SaveChangesAsync();
        }

        public bool Delete(TId id)
        {
            TType entity = this.GetById(id);
            if (entity == null)
            {
                return false;
            }

            this._dbSet.Remove(entity);
            this._dbContext.SaveChanges();

            return true;
        }

        public async Task<bool> DeleteAsync(TId id)
        {
            TType entity = await this.GetByIdAsync(id);
            if (entity == null)
            {
                return false;
            }

            this._dbSet.Remove(entity);
            await this._dbContext.SaveChangesAsync();

            return true;
        }

        public bool Update(TType item)
        {
            try
            {
                this._dbSet.Attach(item);
                this._dbContext.Entry(item).State = EntityState.Modified;
                this._dbContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(TType item)
        {
            try
            {
                this._dbSet.Attach(item);
                this._dbContext.Entry(item).State = EntityState.Modified;
                await this._dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
