namespace Uniqlo2.Models
{
    public class ProductImage:BaseEntity
    {
        public string FileUrl { get; set; }
        public int ProductId { get; set; }
        public Product product { get; set; }
    }
}
