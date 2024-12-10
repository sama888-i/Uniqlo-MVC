using System.ComponentModel.DataAnnotations;

namespace Uniqlo2.Models
{
    public class ProductComment:BaseEntity 
    {
     
        [MaxLength(500)]
        public string? Comment { get; set; }
        public int? ProductId { get; set; }
        public Product? Product { get; set; }
        public string? UserId { get; set; }
        public User? User { get; set; }
      

    }
}
