using System.ComponentModel.DataAnnotations;
using ZalexInc.Certification.Domain.Enums;

namespace ZalexInc.Certification.Domain.Entities
{
    public class Certificate : BaseEntity
    {
        [Required]
        public string AddressTo { get; set; }
        [Required]
        public string Purpose { get; set; }
        public CertificateStatus? Status { get; set; }
        public string? ReferenceNo { get; set; }
        [Required]
        public DateTime IssuedOn { get; set; }
        [Required]
        public string EmployeeId { get; set; }
        public byte[]? PdfFile { get; set; }
    }
}
