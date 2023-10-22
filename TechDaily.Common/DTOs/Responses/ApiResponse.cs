using System;
using System.Collections.Generic;
using System.Text;

namespace TechDaily.Common.DTOs.Responses
{
    public class ApiResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }

        public static ApiResponse Success(string message)
        {
            return new ApiResponse { Status = true, Message = message };
        }

        public static ApiResponse Failure(string message)
        {
            return new ApiResponse { Status = false, Message = message };
        }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T Data { get; set; }

        public static ApiResponse<T> Success(T data, string message)
        {
            return new ApiResponse<T> { Status = true, Message = message, Data = data };
        }
    }
}
