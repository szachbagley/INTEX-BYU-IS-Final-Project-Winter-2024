namespace Intex_Group3_6.Models.ViewModels;

public class AdminOrdersViewModel 
{
    public PaginationInfo PaginationInfo { get; set; } = new PaginationInfo();
    public IQueryable<UserOrders> UserOrders { get; set; }
}