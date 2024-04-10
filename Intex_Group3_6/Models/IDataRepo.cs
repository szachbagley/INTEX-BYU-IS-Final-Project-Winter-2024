
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

        IEnumerable<AvgRating> AvgRatings { get; }

        public IEnumerable<RatedProducts> GetRatingsWithPictures();
    }

}