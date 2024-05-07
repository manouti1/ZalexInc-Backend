using Microsoft.EntityFrameworkCore;
using ZalexInc.Certification.Domain.Entities;
using ZalexInc.Certification.Domain.Interfaces;
using ZalexInc.Certification.Domain.Request;
using ZalexInc.Certification.Infrastructure.Data;

namespace ZalexInc.Certification.Infrastructure.Repositories
{
    public class CeritifcateRepository : ICertificateRepository
    {
        private readonly AppDbContext _context;
        public CeritifcateRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Certificate> CreateCertificateAsync(Certificate certificate)
        {
            _context.Certificates.Add(certificate);
            await _context.SaveChangesAsync();
            return certificate;
        }

        public async Task<bool> DeleteCertificateAsync(Guid certificateId)
        {
            var certificate = await _context.Certificates.FindAsync(certificateId);
            if (certificate == null)
                return false;

            _context.Certificates.Remove(certificate);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Certificate> GetAllCertificateByIdAsync(Guid Id)
        {
            return await _context.Certificates.FindAsync(Id);
        }

        public async Task<List<Certificate>> GetAllCertificatesAsync()
        {
            return await _context.Certificates.ToListAsync();
        }

        public async Task<int> GetTotalCertificatesCountAsync()
        {
            return await _context.Certificates.CountAsync();
        }

        public async Task<List<Certificate>> GetCertificatesPaginatedAsync(int pageIndex, int pageSize, string sortBy, CertificateFilter filter = null)
        {
            IQueryable<Certificate> query = _context.Certificates;


            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.ReferenceNo))
                    query = query.Where(c => c.ReferenceNo == filter.ReferenceNo);
                if (!string.IsNullOrEmpty(filter.AddressTo))
                    query = query.Where(c => c.AddressTo.Contains(filter.AddressTo));
                if (filter.Status != null)
                    query = query.Where(c => c.Status == filter.Status);
            }

            // Apply sorting based on the specified field
            switch (sortBy.ToLower())
            {
                case "issuedon":
                    query = query.OrderBy(c => c.IssuedOn);
                    break;
                case "status":
                    query = query.OrderBy(c => c.Status);
                    break;
                default:
                    query = query.OrderBy(c => c.Id);
                    break;
            }

            query = query.Skip((pageIndex - 1) * pageSize)
                         .Take(pageSize);

            return await query.ToListAsync();
        }

        public async Task<Certificate> UpdateCertificateAsync(Certificate certificate)
        {
            _context.Certificates.Update(certificate);
            await _context.SaveChangesAsync();
            return certificate;
        }
    }
}
