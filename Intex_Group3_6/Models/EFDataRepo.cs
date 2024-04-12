using Intex_Group3_6.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML.OnnxRuntime.Tensors;
using Microsoft.ML.OnnxRuntime;

namespace Intex_Group3_6.Models;

// Class EFDataRepo implementing the IDataRepo interface for database operations
public class EFDataRepo : IDataRepo
{
    private DataContext _context; // The database context used for database operations
    private UserManager<IdentityUser> _userManager; // UserManager from ASP.NET Identity for managing users

    // Constructor that accepts a DataContext instance, initializing the _context field
    public EFDataRepo(DataContext temp)
    {
        _context = temp;
    }

    // Adds a user to the database context
    public void AddUser(User user)
    {
        _context.Users.Add(user); // Add the user object to the Users DbSet
    }

    // Saves all changes made in the context to the database
    public void SaveChanges()
    {
        _context.SaveChanges(); // Persist changes to the database
    }

    // Retrieves a queryable of User objects based on the email address
    public IQueryable<User> GetUser(string email)
    {
        // Return a queryable where users are filtered by the provided email
        var user = _context.Users.Where(e => e.email == email);
        return user;
    }

    // A property to get average ratings directly from the context
    public IEnumerable<AvgRating> AvgRatings => _context.AvgRatings;

    // Returns an IEnumerable of RatedProducts, including products with their average ratings and ordered by the rating
    public IEnumerable<RatedProducts> GetRatingsWithPictures()
    {
        var query = (from rating in _context.AvgRatings
            join product in _context.Products on rating.productId equals product.productId
            orderby rating.avgRating descending // Order by average rating in descending order
            select new RatedProducts
            {
                productId = product.productId,
                productName = product.productName,
                imgLink = product.imgLink,
                price = product.price,
                avgRating = rating.avgRating
            }).Take(9); // Limit the results to the top 9

        return query.ToList(); // Execute the query and convert results to List
    }

    public ProductDetailViewModel GetProductDetail(string productName)
    {
        // This query joins the Products table with the ItemRecs table based on the product name. It filters the products by the specified name and selects a comprehensive set of details from both tables.
        var query = (from product in _context.Products
            join itemrec in _context.ItemRecs on product.productName equals itemrec.item
            where product.productName == productName
            select new
            {
                // Selection of product attributes
                productId = product.productId,
                productName = product.productName,
                year = product.year,
                numParts = product.numParts,
                price = product.price,
                imgLink = product.imgLink,
                primaryColor = product.primaryColor,
                secondaryColor = product.secondaryColor,
                description = product.description,
                category1 = product.category1,
                category2 = product.category2,
                category3 = product.category3,
                // Selection of recommended product names from the related ItemRecs table
                rec1Name = itemrec.rec1,
                rec2Name = itemrec.rec2,
                rec3Name = itemrec.rec3,
                rec4Name = itemrec.rec4,
                rec5Name = itemrec.rec5,
                rec6Name = itemrec.rec6,
                rec7Name = itemrec.rec7,
                rec8Name = itemrec.rec8,
                rec9Name = itemrec.rec9,
                rec10Name = itemrec.rec10
            }).SingleOrDefault(); // Ensures the query returns a single result or null

        // Subsequent queries fetch details for recommended products based on the names retrieved from the first query. Each query fetches the name, ID, and image URL of the recommended product.
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

        // Assembles the final ProductDetailViewModel using the data retrieved above.
        // This model includes detailed information about the product along with a list of recommendations.
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
            rec8Id = rec8.recId,
            rec8Img = rec8.recImg,
            rec9Name = rec9.recName,
            rec9Id = rec9.recId,
            rec9Img = rec9.recImg,
            rec10Name = rec10.recName,
            rec10Id = rec10.recId,
            rec10Img = rec10.recImg
        };

        return model;

    }

    public AdminOrdersViewModel GetOrders(int pageNum)
    {
        int pageSize = 100; // Defines the number of orders to display per page.
        // Retrieves orders marked as fraud and joins them with corresponding user details.
        var query = _context.Orders.Where(o => o.fraud == "1")
            .Join(_context.Users,
                order => order.userId,
                user => user.userId,
                (order, user) => new UserOrders { Orders = order, Users = user });

        // Applies pagination to the query, skipping the previous pages' items and taking the current page's items.
        var pagedOrders = query.Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();

        // Constructs the view model to be returned, including the paginated orders and pagination details.
        var model = new AdminOrdersViewModel
        {
            UserOrders = pagedOrders.AsQueryable(),
            PaginationInfo = new PaginationInfo
            {
                TotalItems = query.Count(), // Total number of orders marked as fraud
                ItemsPerPage = pageSize,
                CurrentPage = pageNum,
            }
        };

        return model;
    }

    public AdminUsersViewModel GetUsers(int pageNum)
    {
        int pageSize = 50; // Number of users to display per page.
        // Retrieves all users.
        var query = _context.Users;

        // Pagination applied to the user query.
        var pagedUsers = query.Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();

        // Constructs the view model, including paginated users and pagination details.
        var model = new AdminUsersViewModel
        {
            User = pagedUsers.AsQueryable(),
            PaginationInfo = new PaginationInfo
            {
                TotalItems = query.Count(), // Total number of users
                ItemsPerPage = pageSize,
                CurrentPage = pageNum,
            }
        };

        return model;
    }

    public AdminProductsViewModel GetProducts(int pageNum, int pageSize)
    {
        // Retrieves all products.
        var query = _context.Products;
        // Applies pagination to the product query.
        var pagedProducts = query
            .Skip((pageNum - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        // Constructs the view model, including paginated products and pagination details.
        var model = new AdminProductsViewModel
        {
            Products = pagedProducts.AsQueryable(),
            PaginationInfo = new PaginationInfo
            {
                TotalItems = query.Count(), // Total number of products
                ItemsPerPage = pageSize,
                CurrentPage = pageNum,
            }
        };

        return model;
    }

    public IQueryable<Product> Products => _context.Products; // Provides a queryable interface to products.

    public Product GetProductById(int productId)
    {
        // Retrieves a single product by its ID.
        return _context.Products.FirstOrDefault(p => p.productId == productId);
    }

    public void UpdateProduct(Product product) // Updates an existing product in the database.
    {
        _context.Products.Update(product);
    }

    public void DeleteProduct(Product product)
    {
        // Attempt to find the product in the database by its ID.
        var productToDelete = _context.Products.FirstOrDefault(p => p.productId == product.productId);
        // If the product is found, remove it from the database context.
        if (productToDelete != null)
        {
            _context.Products.Remove(productToDelete);
        }
    }

    public User GetUserById(int userId)
    {
        // Retrieve a user by their ID using a straightforward query.
        return _context.Users.FirstOrDefault(p => p.userId == userId);
    }

    public void UpdateUser(User user) // Method to update existing user details in the database.
    {
        // Updates the user entity within the database context.
        _context.Users.Update(user);
    }

    public void DeleteUser(User user)
    {
        // Similar to DeleteProduct, it first finds the user by ID.
        var userToDelete = _context.Users.FirstOrDefault(p => p.userId == user.userId);
        // If found, it removes the user from the database.
        if (userToDelete != null)
        {
            _context.Users.Remove(userToDelete);
        }
    }

    public Order GetOrderById(int transactionId)
    {
        // Fetches an order based on the transaction ID.
        return _context.Orders.FirstOrDefault(o => o.transactionId == transactionId);
    }

    public void DeleteOrder(Order order)
    {
        // Finds the order to delete.
        var orderToDelete = _context.Orders.FirstOrDefault(o => o.transactionId == order.transactionId);
        // Also finds related line items that need to be deleted.
        var lineItemsToDelete = _context.LineItems.Where(line => line.TransactionId == order.transactionId);
        // Removes the found order.
        if (orderToDelete != null)
        {
            _context.Orders.Remove(orderToDelete);
        }

        // Removes all associated line items.
        if (lineItemsToDelete.Any()) // Ensure there are line items to remove.
        {
            _context.LineItems.RemoveRange(lineItemsToDelete);
        }
    }

    public void AddProduct(Product product)
    {
        // Adds a new product to the database.
        _context.Products.Add(product);
    }

    public User GetUserByEmail(string email)
    {
        // Retrieves a user by their email address.
        return _context.Users.FirstOrDefault(p => p.email == email);
    }

    public void AddLineItem(LineItem lineItem)
    {
        // Adds a new line item to the database.
        _context.LineItems.Add(lineItem);
    }

    public void AddOrder(Order order)
    {
        // Adds a new order to the database.
        _context.Orders.Add(order);
    }

    public IQueryable<Product> GetUserRec(int userId)
    {
        // Retrieves user-specific recommendations based on a record in UserRecs table.
        var userRec = _context.UserRecs.FirstOrDefault(u => u.userId == userId);

        List<string> recommendations = new List<string>();

        if (userRec != null)
        {
            // If there are recommendations, add them to the list.
            recommendations.AddRange(new List<string>
            {
                userRec.rec1,
                userRec.rec2,
                userRec.rec3,
                userRec.rec4,
                userRec.rec5
            });
        }

        // Query products that match the list of recommended product names.
        var products = _context.Products.Where(p => recommendations.Contains(p.productName));

        return products;
    }
}