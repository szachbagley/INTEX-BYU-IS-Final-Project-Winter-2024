using System.ComponentModel.DataAnnotations;

namespace Intex_Group3_6.Models;

public class Product
{
    [Key]
    [Required]
    public int productId { get; set; }
    public string? productName { get; set; }
    public int year { get; set; }
    public int numParts { get; set; }
    public float price { get; set; }
    public string? imgLink { get; set; }
    public string? primaryColor { get; set; }
    public string? secondaryColor { get; set; }
    public string? description { get; set; }
    public string? category1 { get; set; }
    public string? category2 { get; set; }
    public string? category3 { get; set; }
}