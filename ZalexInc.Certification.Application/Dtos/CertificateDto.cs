using ZalexInc.Certification.Domain.Enums;

namespace ZalexInc.Certification.Application.Dtos
{
    public class CertificateDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string AddressTo { get; set; }
        public string Purpose { get; set; }
        public CertificateStatus? Status { get; set; }
        public string? ReferenceNo { get; set; }
        public DateTime IssuedOn { get; set; }
        public string EmployeeId { get; set; }
        public byte[]? PdfFile { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
