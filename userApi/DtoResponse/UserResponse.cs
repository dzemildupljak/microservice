using System.Collections.Generic;

namespace userApi.DtoResponse
{
    public class UserResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
    }
}