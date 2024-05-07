namespace ZalexInc.Certification.Domain.Utils
{
    public class OperationResult
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public List<string> Errors { get; set; }

        protected OperationResult(bool success, string message)
        {
            Success = success;
            Message = message;
            Errors = new List<string>();
        }

        public static OperationResult Ok()
        {
            return new OperationResult(true, "Operation completed successfully.");
        }

        public static OperationResult Fail(string message)
        {
            return new OperationResult(false, message);
        }


        public static OperationResult Fail(List<string> errors)
        {
            var result = new OperationResult(false, "Operation failed with errors.");
            result.Errors = errors;
            return result;
        }
    }

    public class OperationResult<T> : OperationResult
    {
        public T Data { get; private set; }

        private OperationResult(bool success, string message, T data) : base(success, message)
        {
            Data = data;
        }

        public static OperationResult<T> Ok(T data)
        {
            return new OperationResult<T>(true, "Operation completed successfully.", data);
        }

        public static OperationResult<T> Fail(string message, T data)
        {
            return new OperationResult<T>(false, message, data);
        }

        public static OperationResult<T> Fail(string message)
        {
            return new OperationResult<T>(false, message, default(T));
        }

        public static OperationResult<T> Fail(string message, List<string> errors)
        {
            var result = new OperationResult<T>(false, message, default);
            result.Errors = errors;
            return result;
        }


    }

}
