namespace Identity.Api.Model
{
    public class SignUpUserDto
    {
        public required string Email { get; set; }
        public  required string UserName { get; set; }
        public required string Password { get; set; }

    }
}
