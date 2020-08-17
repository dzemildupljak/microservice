using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace userApi.JWT
{
    public class JwtConst
    {
        public const string Issuer = "ApiIssuer";
        public const string Audience = "ApiUser";
        public const string Key = "1234567890123456";

    }
}