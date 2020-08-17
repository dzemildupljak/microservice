using System.ComponentModel.DataAnnotations;

namespace userApi.Dto
{
    public class DtoUserRegister
    {
        public string Name { get; set; }
        public string Username { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string Role { get; set; }
    }
}