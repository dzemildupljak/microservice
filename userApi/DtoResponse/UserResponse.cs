using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

namespace userApi.DtoResponse
{
    public class UserResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public string JwtResponseToken { get; set; }
        public DateTime ExpireToToken { get; set; }

    }
}