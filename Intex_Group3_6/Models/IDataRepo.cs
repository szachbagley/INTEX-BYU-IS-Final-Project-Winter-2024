using System.Collections.Generic;
using System.Linq;

namespace Intex_Group3_6.Models
{
    void AddUser(User user);
    void SaveChanges();

    public interface IDataRepo
    {
        public IQueryable<Product> Products { get; }

        IEnumerable<AvgRating> AvgRatings { get; }

        public IEnumerable<RatedProducts> GetRatingsWithPictures();
    }
}