namespace Intex_Group3_6.Models.ViewModels
{
    public class ProductPageViewModel
    {
        public IQueryable<Product> Products { get; set;}
        public PaginationInfo PaginationInfo { get; set; } = new PaginationInfo();
        
        public ProductFilterOptions FilterOptions { get; set; }
    }
}
