using System.ComponentModel.DataAnnotations;

namespace Intex_Group3_6.Models;

public class Product
{
    [Key]
    public required int productId { get; set; }
    public required string productName { get; set; }
    public required int year { get; set; }
    public required int numParts { get; set; }
    public required float price { get; set; }
    public required string imgLink { get; set; }
    public required string primaryColor { get; set; }
    public required string secondaryColor { get; set; }
    public required string description { get; set; }
    private string? category1 { get; set; }
    private string? category2 { get; set; }
    private string? category3 { get; set; }
}