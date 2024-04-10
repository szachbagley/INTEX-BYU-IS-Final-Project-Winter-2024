using Intex_Group3_6.Models.ViewModels;

namespace Intex_Group3_6.Models;

public class EFDataRepo : IDataRepo
{
    private DataContext _context;
    public EFDataRepo(DataContext temp) 
    { 
        _context = temp;
    }
    
    public void AddUser(User user) // Use this to add a user to the database
    {
        _context.Users.Add(user);
    }
    
    public void SaveChanges() // Use this to save any changes to the database
    {
        _context.SaveChanges();
    }

    public IQueryable<User> GetUser(string email)
    {
        var user = _context.Users.Where(e => e.email == email);
        return user;
    }

    public IEnumerable<AvgRating> AvgRatings => _context.AvgRatings;

    public IEnumerable<RatedProducts> GetRatingsWithPictures()
    {
        var query = (from rating in _context.AvgRatings
                         join product in _context.Products on rating.productId equals product.productId
                     orderby rating.avgRating descending
                     select new RatedProducts
                         {
                             productId = product.productId,
                             productName = product.productName,
                             imgLink = product.imgLink,
                             price = product.price,
                             avgRating = rating.avgRating

                         }).Take(9);


        return query.ToList();
    }

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



    public IQueryable<Product> Products => _context.Products;

}