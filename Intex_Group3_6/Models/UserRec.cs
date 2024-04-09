using System.ComponentModel.DataAnnotations;

namespace Intex_Group3_6.Models;

public class UserRec
{
    [Key]
    public required int userId { get; set; }
    public string? rec1 { get; set; }
    public string? rec2 { get; set; }
    public string? rec3 { get; set; }
    public string? rec4 { get; set; }
    public string? rec5 { get; set; }
}