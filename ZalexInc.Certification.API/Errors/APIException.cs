namespace ZalexInc.Certification.API.Errors
{
    public class APIException : APIResponce
    {
        public APIException(int StatusCode, string message = null, string details = "") : base(StatusCode, message)
        {
            Details = details; // Correctly assign the Details property
        }
        public string Details { get; set; }

    }
}
