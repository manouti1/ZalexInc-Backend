using MediatR;
using ZalexInc.Certification.Application.Dtos;
using ZalexInc.Certification.Domain.Enums;
using ZalexInc.Certification.Domain.Utils;

namespace ZalexInc.Certification.Application.Commands
{
    public class CreateCertificateCommand : IRequest<OperationResult<CertificateDto>>
    {
        public string AddressTo { get; set; }
        public string Purpose { get; set; }
        public CertificateStatus? Status { get; set; }
        public string? ReferenceNo { get; set; }
        public DateTime IssuedOn { get; set; }
        public int EmployeeId { get; set; }
        public byte[]? PdfFile { get; set; }
    }
}
