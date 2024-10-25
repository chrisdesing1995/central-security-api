
namespace CentralSecurity.Domain.Common
{
    public class ResponseResult<T>
    {
        public T Result { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }

        public ResponseResult()
        {
            Status = true;
        }

        public ResponseResult(T result) : this()
        {
            Result = result;
        }

        public ResponseResult(string message, bool status = false)
        {
            Message = message;
            Status = status;
        }

        public ResponseResult(T result, string message, bool status = false)
        {
            Result = result;
            Message = message;
            Status = status;
        }
    }
}
