using System.ComponentModel.DataAnnotations;

namespace MyApp.Models
{
    public class User
    {
        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Wrong Email.")]
        public string Email { get; set; }
        public string Password { get; set; }

        public User(int id, string userName, string email, string password)
        {
            UserName = userName;
            Email = email;
            Password = password;
        }
        public User() { }
    }
}
