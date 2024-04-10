using Intex_Group3_6.Models.ViewModels;

namespace Intex_Group3_6.Models;

public class EFDataRepo : IDataRepo
{
    private DataContext _context;
    public EFDataRepo(DataContext temp) 
    { 
        _context = temp;
    }

    // public IQueryable<AdminOrdersViewModel> GetOrders(int pageNum)
    // {
    //     var fraudulentOrders = _context.Orders
    //         .Where(o => o.fraud == true)
    //         .Join(_context.Users,
    //             order => order.userId,
    //             user => user.userId,
    //             (order, user) => new UserOrders { Orders = order, Users = user })
    //         .AsQueryable();
    //
    //     // Assuming you have logic to calculate pagination info, like total items etc.
    //     var paginationInfo = new PaginationInfo
    //     {
    //         // Populate your pagination info here
    //         TotalItems = _context.Orders.Count(),
    //         ItemsPerPage = 100,
    //         CurrentPage = pageNum
    //     };
    //
    //     var adminOrdersViewModel = new AdminOrdersViewModel
    //     {
    //         UserOrders = fraudulentOrders,
    //         PaginationInfo = paginationInfo
    //     };
    //
    //     return new[] { adminOrdersViewModel }.AsQueryable(); // You can use AsQueryable to convert the array to IQueryable
    // }
    public AdminOrdersViewModel GetOrders(int pageNum)
    {
        int pageSize = 100; // Or whatever your page size should be
        var query = _context.Orders.Where(o => o.fraud == true)
            .Join(_context.Users,
                order => order.userId,
                user => user.userId,
                (order, user) => new UserOrders { Orders = order, Users = user });

        // Here you would add pagination logic to the query
        var pagedOrders = query.Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();

        var model = new AdminOrdersViewModel
        {
            UserOrders = pagedOrders.AsQueryable(),
            PaginationInfo = new PaginationInfo
            {
                // Assign the properties of PaginationInfo accordingly
                TotalItems = query.Count(),
                ItemsPerPage = pageSize,
                CurrentPage = pageNum,
            }
        };

        return model;
    }


}