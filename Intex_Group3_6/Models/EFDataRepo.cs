namespace Intex_Group3_6.Models;

public class EFDataRepo : IDataRepo
{
    private DataContext _context;
    public EFDataRepo(DataContext temp) 
    { 
        _context = temp;
    }
    public IEnumerable<object> GetTop10ReviewedSets()
    {
        var joinedData = from AvgRating in _context.AvgRatings
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

        var joinedList = joinedData.ToArray();

       


        return joinedList.OrderByDescending(r => r.AvgRating).Take(10);
    }

}