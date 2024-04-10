using Intex_Group3_6.Models.ViewModels;

namespace Intex_Group3_6.Models;

public interface IDataRepo
{
    public AdminOrdersViewModel GetOrders(int pageNum);
}