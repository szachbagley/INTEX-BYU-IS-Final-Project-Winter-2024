using System.ComponentModel.DataAnnotations;

namespace Intex_Group3_6.Models
{
    public class AvgRating
    {
        [Key]
        public int productId { get; set; }

        public int avgRating { get; set; }
    };

}
