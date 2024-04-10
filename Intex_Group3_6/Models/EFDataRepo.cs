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

    public IEnumerable<AvgRating> AvgRatings => _context.AvgRatings;

    public IEnumerable<RatedProducts> GetRatingsWithPictures()
    {
        var query = (from rating in _context.AvgRatings
                         join product in _context.Products on rating.productId equals product.productId
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

    public IQueryable<Product> Products => _context.Products;
}