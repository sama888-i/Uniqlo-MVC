namespace Uniqlo2.ViewModels.Product
{
    public class ProductVm
    {
        public string Name { get; set; } = null!;
        public string CoverImage { get; set; }
        public string CategoryName { get; set; }
        public string CostPrice { get; set; } = null!;
        public string SellPrice { get; set; }=null!;
        public string Discount {get;set;}=null!;
        public string Quantity {get;set;}=null!;
        
    } 
}
