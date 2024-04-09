using System.ComponentModel.DataAnnotations;

namespace Intex_Group3_6.Models;

public class ItemRec
{
    [Key]
    public required string item { get; set; }
    public string? rec1 { get; set; }
    public string? rec2 { get; set; }
    public string? rec3 { get; set; }
    public string? rec4 { get; set; }
    public string? rec5 { get; set; }
    public string? rec6 { get; set; }
    public string? rec7 { get; set; }
    public string? rec8 { get; set; }
    public string? rec9 { get; set; }
    public string? rec10 { get; set; }
}