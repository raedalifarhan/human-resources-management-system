namespace API.DTOs
{
    // Contain properties return back after loging or register.
    public class UserDto
    {
        public string DisplayName { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public string Image { get; set; }
    }
}