using System.ComponentModel.DataAnnotations;

namespace MyApp.ViewModels
{
    public record class OrdersCount([Required][Range(0, int.MaxValue, ErrorMessage = "Amount must be a non-negative number.")] string Count);
}
