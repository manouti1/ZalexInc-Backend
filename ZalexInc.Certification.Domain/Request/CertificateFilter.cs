using ZalexInc.Certification.Domain.Enums;

namespace ZalexInc.Certification.Domain.Request
{
    public class CertificateFilter
    {
        public string ReferenceNo { get; set; }
        public string AddressTo { get; set; }
        public CertificateStatus? Status { get; set; }
    }

}
