using System.ComponentModel.DataAnnotations;

namespace Intex_Group3_6.Models;

public class LineItem
{
    [Key]
    public required int transactionId { get; set; }
    public required int productId { get; set; }
    public required int quantity { get; set; }
    public int? rating { get; set; }
}