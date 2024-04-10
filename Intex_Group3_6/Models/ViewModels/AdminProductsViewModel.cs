namespace Intex_Group3_6.Models.ViewModels;

public class AdminProductsViewModel
{
    public PaginationInfo PaginationInfo { get; set; } = new PaginationInfo();
    
    public IQueryable<Product> Products { get; set; }
}