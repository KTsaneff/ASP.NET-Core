namespace CinemaWebApp.Data.Repository.Contracts
{
    public interface IRepository<TType, TId>
    {
        TType GetById(TId id);

        Task<TType> GetByIdAsync(TId id);

        IEnumerable<TType> GetAll();

        Task<IEnumerable<TType>> GetAllAsync();

        void Add(TType item);

        Task AddAsync(TType item);

        bool Delete(TId id);

        Task<bool> DeleteAsync(TId id);

        bool SoftDelete(TId id);

        Task<bool> SoftDeleteAsync(TId id);

        bool Update(TType item);

        Task<bool> UpdateAsync(TType item);
    }
}
