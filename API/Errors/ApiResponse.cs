using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefMsg(statusCode);
        }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        private string GetDefMsg(int statusCode)
        {
            return statusCode switch
            {
                400 => "Sorry, This is a bad request",
                401 => "Sorry, you are not Authorized to access this request",
                404 => "Sorry, This request is not found",
                500 => "Sorry, This request caused an internal server error",
                _ => null
            };
        }
    }
}
