using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cowboy.Services
{
    /// <summary>
    /// Generic wrapper for web api response.       
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = "OK";

        public ServiceResponse() { }
        public ServiceResponse(T data)
        {
            Data = data;
        }

        public ServiceResponse(string message, bool success)
        {
            Message = message;
            Success = success;
        }
    }
}
