using MediatR;
using ZalexInc.Certification.Application.Dtos;
using ZalexInc.Certification.Application.Helpers;
using ZalexInc.Certification.Domain.Request;
using ZalexInc.Certification.Domain.Utils;

namespace ZalexInc.Certification.Application.Queries
{
    public class GetPaginatedCertificatesQuery : IRequest<OperationResult<Pagination<CertificateDto>>>
    {
        public int PageIndex { get; }
        public int PageSize { get; }
        public string SortBy { get; }
        public CertificateFilter Filter { get; }

        public GetPaginatedCertificatesQuery(int pageIndex, int pageSize, string sortBy, CertificateFilter filter = null)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            SortBy = sortBy;
            Filter = filter;
        }
    }
}
