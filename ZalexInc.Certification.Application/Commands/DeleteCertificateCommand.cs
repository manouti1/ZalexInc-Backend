using MediatR;
using ZalexInc.Certification.Domain.Utils;

namespace ZalexInc.Certification.Application.Commands
{
    public class DeleteCertificateCommand : IRequest<OperationResult>
    {
        public Guid Id { get; set; }
    }
}
