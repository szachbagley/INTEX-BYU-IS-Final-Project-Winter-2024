using System.Collections.Generic;
using System.Linq;

namespace Intex_Group3_6.Models
{
    public interface IDataRepo
    {
        void AddUser(User user);
        void SaveChanges();
        public IQueryable<Product> Products { get; }

        IEnumerable<AvgRating> AvgRatings { get; }

        public IEnumerable<RatedProducts> GetRatingsWithPictures();
    }
}