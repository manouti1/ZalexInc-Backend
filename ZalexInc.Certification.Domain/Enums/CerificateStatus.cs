namespace ZalexInc.Certification.Domain.Enums
{
    public enum CertificateStatus
    {
        Pending,        // The request is pending processing
        Approved,       // The request has been approved
        Rejected,       // The request has been rejected
        Processing,     // The request is currently being processed
        Completed       // The request has been completed
    }
}

