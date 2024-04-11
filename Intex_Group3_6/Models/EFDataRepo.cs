using Intex_Group3_6.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

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

    public ProductDetailViewModel GetProductDetail(string productName)
    {
        var query = (from  product in _context.Products
                     join itemrec in _context.ItemRecs on product.productName equals itemrec.item
                     where product.productName == productName
                     select new
                     {
                         productId = product.productId,
                         productName = product.productName,
                         year = product.year,
                         numParts = product.numParts,
                         price = product.price,
                         imgLink= product.imgLink,
                         primaryColor = product.primaryColor,
                         secondaryColor = product.secondaryColor,
                         description = product.description,
                         category1 = product.category1,
                         category2 = product.category2,
                         category3 = product.category3,
                         rec1Name = itemrec.rec1,
                         rec2Name = itemrec.rec2,
                         rec3Name = itemrec.rec3,
                         rec4Name = itemrec.rec4,
                         rec5Name = itemrec.rec5,
                         rec6Name = itemrec.rec6,
                         rec7Name = itemrec.rec7,
                         rec8Name = itemrec.rec8,
                         rec9Name = itemrec.rec9,
                         rec10Name = itemrec.rec10,
                     }).SingleOrDefault();

            var rec1 = (from product in _context.Products
                        where product.productName == query.rec1Name
                        select new
                        {
                            recName = product.productName,
                            recId = product.productId,
                            recImg = product.imgLink
                        }).SingleOrDefault();

            var rec2 = (from product in _context.Products
                        where product.productName == query.rec2Name
                        select new
                        {
                            recName = product.productName,
                            recId = product.productId,
                            recImg = product.imgLink
                        }).SingleOrDefault();

            var rec3 = (from product in _context.Products
                        where product.productName == query.rec3Name
                        select new
                        {
                            recName = product.productName,
                            recId = product.productId,
                            recImg = product.imgLink
                        }).SingleOrDefault();
            var rec4 = (from product in _context.Products
                        where product.productName == query.rec4Name
                        select new
                        {
                            recName = product.productName,
                            recId = product.productId,
                            recImg = product.imgLink
                        }).SingleOrDefault();
            var rec5 = (from product in _context.Products
                        where product.productName == query.rec5Name
                        select new
                        {
                            recName = product.productName,
                            recId = product.productId,
                            recImg = product.imgLink
                        }).SingleOrDefault();
            var rec6 = (from product in _context.Products
                        where product.productName == query.rec6Name
                        select new
                        {
                            recName = product.productName,
                            recId = product.productId,
                            recImg = product.imgLink
                        }).SingleOrDefault();
            var rec7 = (from product in _context.Products
                        where product.productName == query.rec7Name
                        select new
                        {
                            recName = product.productName,
                            recId = product.productId,
                            recImg = product.imgLink
                        }).SingleOrDefault();
            var rec8 = (from product in _context.Products
                        where product.productName == query.rec8Name
                        select new
                        {
                            recName = product.productName,
                            recId = product.productId,
                            recImg = product.imgLink
                        }).SingleOrDefault();
            var rec9 = (from product in _context.Products
                        where product.productName == query.rec9Name
                        select new
                        {
                            recName = product.productName,
                            recId = product.productId,
                            recImg = product.imgLink
                        }).SingleOrDefault();
            var rec10 = (from product in _context.Products
                        where product.productName == query.rec10Name
                        select new
                        {
                            recName = product.productName,
                            recId = product.productId,
                            recImg = product.imgLink
                        }).SingleOrDefault();

        ProductDetailViewModel model = new ProductDetailViewModel
        {
            productId = query.productId,
            productName = query.productName,
            year = query.year,
            numParts = query.numParts,
            price = query.price,
            imgLink = query.imgLink,
            primaryColor = query.primaryColor,
            secondaryColor = query.secondaryColor,
            description = query.description,
            category1 = query.category1,
            category2 = query.category2,
            category3 = query.category3,
            rec1Name = rec1.recName,
            rec1Id = rec1.recId,
            rec1Img = rec1.recImg,
            rec2Name = rec2.recName,
            rec2Id = rec2.recId,
            rec2Img = rec2.recImg,
            rec3Name = rec3.recName,
            rec3Id = rec3.recId,
            rec3Img = rec3.recImg,
            rec4Name = rec4.recName,
            rec4Id = rec4.recId,
            rec4Img = rec4.recImg,
            rec5Name = rec5.recName,
            rec5Id = rec5.recId,
            rec5Img = rec5.recImg,
            rec6Name = rec6.recName,
            rec6Id = rec6.recId,
            rec6Img = rec6.recImg,
            rec7Name = rec7.recName,
            rec7Id = rec7.recId,
            rec7Img = rec7.recImg,
            rec8Name = rec8.recName,
            rec8Id = rec8.recId
        };

        return model;
        
    }

    public AdminOrdersViewModel GetOrders(int pageNum)
    {
        int pageSize = 100; // Or whatever your page size should be
        var query = _context.Orders.Where(o => o.fraud == "1")
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

    public AdminUsersViewModel GetUsers(int pageNum)
    {
        int pageSize = 50; // Or whatever your page size should be
        var query = _context.Users;


        // Here you would add pagination logic to the query
        var pagedOrders = query.Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();

        var model = new AdminUsersViewModel
        {
            User = pagedOrders.AsQueryable(),
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
    
    public AdminProductsViewModel GetProducts(int pageNum, int pageSize)
    {
        var query = _context.Products;
        var pagedProducts = query
            .Skip((pageNum - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var model = new AdminProductsViewModel
        {
            Products = pagedProducts.AsQueryable(),
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

    public Product GetProductById(int productId)
    {
        return _context.Products.FirstOrDefault(p => p.productId == productId);
    }
    
    public void UpdateProduct(Product product) // Use this to edit products in the database
    {
        _context.Products.Update(product);
    }

    public void DeleteProduct(Product product)
    {
        var productToDelete = _context.Products.FirstOrDefault(p => p.productId == product.productId);
        if (productToDelete != null)
        {
            _context.Products.Remove(productToDelete);
        }
    }
}