using MyApp.Models;

namespace MyApp.ViewModels.HomeViewModles
{
    public enum ShowStyles
    {
        List,
        Table
    }
    public record class ShowOrdersViewModel(IEnumerable<Order> Orders, ShowStyles ShowStyle);
}
