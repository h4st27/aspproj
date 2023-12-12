using System.Xml.Linq;

namespace MyApp.Models
{
    public class Order
    {
        private static int _id = 1;
        public int Id { get; private set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public Order()
        {
            Name = "";
            Id = _id++;
            CreatedAt = DateTime.Now;
        }
        public override string ToString()
        {
            return $"Order Id:{Id} Name:{Name} Price:{Price} CreatedAt:{CreatedAt}";
        }
    }
}
