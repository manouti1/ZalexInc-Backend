using MediatR;
using ZalexInc.Certification.Application.Dtos;
using ZalexInc.Certification.Domain.Utils;

namespace ZalexInc.Certification.Application.Queries
{
    public class GetCertificateByIdQuery : IRequest<OperationResult<CertificateDto>>
    {
        public Guid Id { get; set; }
    }
}
