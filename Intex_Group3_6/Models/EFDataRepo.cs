namespace Intex_Group3_6.Models;

public class EFDataRepo : IDataRepo
{
    private DataContext _context;
    public EFDataRepo(DataContext temp) 
    { 
        _context = temp;
    }
    public IQueryable<Object> GetTop10ReviewedSets()
    {
        IQueryable<Object> joinedData = from AvgRating in _context.AvgRatings
                         join Product in _context.Products
                         on AvgRating.productId equals Product.productId
                         select new
                         {
                             ProductName = Product.productName,
                             ProductImg = Product.imgLink,
                             ProductPrice = Product.price,
                             ProductId = Product.productId,
                             AvgRating = AvgRating.avgRating
                         };

       


        return joinedData.OrderByDescending(r => r.AvgRating).Take(10);
    }

}