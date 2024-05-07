using ZalexInc.Certification.Domain.Entities;
using ZalexInc.Certification.Domain.Request;

namespace ZalexInc.Certification.Domain.Interfaces
{
    public interface ICertificateRepository
    {
        Task<List<Certificate>> GetAllCertificatesAsync();
        Task<Certificate> GetAllCertificateByIdAsync(Guid Id);
        Task<Certificate> UpdateCertificateAsync(Certificate certificate);
        Task<Certificate> CreateCertificateAsync(Certificate certificate);
        Task<bool> DeleteCertificateAsync(Guid certificateId);
        Task<List<Certificate>> GetCertificatesPaginatedAsync(int pageIndex, int pageSize, string sortBy, CertificateFilter filter = null);
        Task<int> GetTotalCertificatesCountAsync();
    }
}
