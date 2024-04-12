using System.ComponentModel.DataAnnotations;

namespace Intex_Group3_6.Models;
public class User
{
    [Key]
    public required int userId { get; set; }
    public string? firstName { get; set; }
    public string? lastName { get; set; }
    public DateOnly birthDate { get; set; }
    public string? country { get; set; }
    public string? gender { get; set; }
    public int? age { get; set; }
    public string? email { get; set; }
    public string? role { get; set; }
}
