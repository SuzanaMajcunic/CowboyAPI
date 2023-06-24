namespace CowboyAPI.Models
{
    public class ApiResult<T>
    {
        public bool Success { get;}
        public string? ErrorMsg { get; }

        public T? Result { get;}


        public ApiResult(T? result)
        {
            Success = result != null;
            Result = result;
        }

        public ApiResult(string? error)
        {
            Success = false;
            ErrorMsg = error;
        }
    }
}
