using System.ComponentModel.DataAnnotations;

namespace Uniqlo2.Models;

public class Category:BaseEntity 
{

    [MaxLength (32)]
    public string Name { get; set; } = null!;
    public IEnumerable<Product>? Products { get; set; }
}
