using Microsoft.EntityFrameworkCore;

namespace Intex_Group3_6.Models;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base (options)
    { 
    }
    
    public DbSet<ItemRec> ItemRecs { get; set; }
    public DbSet<UserRec> UserRecs { get; set; }
    public DbSet<LineItem> LineItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }

    public DbSet<AvgRating> AvgRatings { get; set; }
}