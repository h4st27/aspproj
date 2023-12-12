namespace MyApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public User User { get; set; }
        public Product()
        {
        }
        public Product(int id, string name, string description, User user)
        {
            Id = id;
            Name = name;
            Description = description;
            User = user;
        }
    }

}
