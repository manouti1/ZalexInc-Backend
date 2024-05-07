using ZalexInc.Certification.Domain.Entities;
using ZalexInc.Certification.Domain.Specifications;

namespace ZalexInc.Certification.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);

        Task<T> GetEntityWithSpec(ISpecifications<T> specification);
        Task<IReadOnlyList<T>> ListAsync(ISpecifications<T> specification);
        Task<int> CountAsync(ISpecifications<T> specifications);
        Task DeleteAsync(T entity);
        Task UpdateAsync(T entity);

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
