using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using MyApp.Models;
using System.Text;

namespace MyApp.ViewComponents
{
    public class TableViewComponent
    {
        public IViewComponentResult Invoke(IEnumerable<Order> orders)
        {
            StringBuilder sb = new();
            foreach (var order in orders)
            {
                sb.Append(order.ToString()+"<br/>");
            }

            return new HtmlContentViewComponentResult(new HtmlString(sb.ToString()));
        }

    }
}
