namespace Intex_Group3_6.Models.ViewModels;

public class AdminUsersViewModel
{
    public PaginationInfo PaginationInfo { get; set; } = new PaginationInfo();
    
    public IQueryable<User> User { get; set; }
}