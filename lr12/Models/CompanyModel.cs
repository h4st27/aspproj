using System.ComponentModel.DataAnnotations.Schema;

namespace lr12.Models
{
    public class CompanyModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Workers { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }

    }
}
