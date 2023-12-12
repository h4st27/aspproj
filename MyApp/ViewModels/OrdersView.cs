using MyApp.Models;

namespace MyApp.ViewModels
{
    public record class OrdersView(IEnumerable<Product>Products);
}
