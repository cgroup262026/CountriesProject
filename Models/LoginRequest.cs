namespace CountriesProject.Models
{
    public class LoginRequest
    {
        string email;
        string password;
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
    }
}
