
using Intex_Group3_6.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Intex_Group3_6.Models
{
    public interface IDataRepo
    {
        void AddUser(User user);
        void SaveChanges();
        
        public AdminOrdersViewModel GetOrders(int pageNum);
      
        public IQueryable<Product> Products { get; }

        public AdminProductsViewModel GetProducts(int pageNum, int pageSize);

        IEnumerable<AvgRating> AvgRatings { get; }

        public IEnumerable<RatedProducts> GetRatingsWithPictures();
        public AdminUsersViewModel GetUsers(int pageNum);
    }

}