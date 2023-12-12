using System.ComponentModel.DataAnnotations;

namespace MyApp.ViewModels
{
    public record class UserView([Required] string Name, [Required] string Password, [Required][Range(0, int.MaxValue, ErrorMessage = "Age must be a non-negative number.")] string Age);
}
