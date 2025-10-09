using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace frist_project_one.Controllers
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Date { get; set; }
        public List<string>? Errors { get; set; }
        public int StatusCode { get; set; }
        public DateTime TimeStamp { get; set; }

        //constructor for success response................ Strat!
        private ApiResponse(bool success, string message, T? data, List<string>? errors, int statusCode)
        {
            Success = success;
            Message = message;
            Date = data;
            Errors = errors;
            StatusCode = statusCode;
            TimeStamp = DateTime.UtcNow;
        }
        //constructor for success response................ End!

        //static method for creating a successfully  response..!
        public static ApiResponse<T> SuccessResponse(T? data, int statusCode, string message = "")
        {
            return new ApiResponse<T>(true, message, data, null, statusCode);
        }
         
         //static method for creating a Errors  response..!
        public static ApiResponse<T>ErrorsResponse(List<string> errors, int statusCode, string message = "")
        {
            return new ApiResponse<T>(false, message, default(T), errors, statusCode);
         }
    }
}