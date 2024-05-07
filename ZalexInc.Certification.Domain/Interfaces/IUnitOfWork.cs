using ZalexInc.Certification.Domain.Entities;

namespace ZalexInc.Certification.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> repository<TEntity>() where TEntity : BaseEntity;
        Task<int> Complete();
    }
}
