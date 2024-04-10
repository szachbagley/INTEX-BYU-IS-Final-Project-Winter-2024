using System.Collections.Generic;
using System.Linq;

namespace Intex_Group3_6.Models
{
    public IQueryable<Product> Products { get; }
  
    public interface IDataRepo
    {
        IEnumerable<AvgRating> AvgRatings { get; }

        public IEnumerable<RatedProducts> GetRatingsWithPictures();
    }
}