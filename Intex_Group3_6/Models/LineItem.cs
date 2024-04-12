using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intex_Group3_6.Models;

public class LineItem
{
    public int TransactionId { get; set; }
    public int ProductId { get; set; }
    public required int quantity { get; set; }
    public int? rating { get; set; }
}