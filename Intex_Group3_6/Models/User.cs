using System.ComponentModel.DataAnnotations;

namespace Intex_Group3_6.Models;

public class User
{
    [Key]
    public required int userId { get; set; }
    public required string firstName { get; set; }
    public required string lastName { get; set; }
    public required DateOnly birthDate { get; set; }
    public required string country { get; set; }
    public required string gender { get; set; }
    public required int age { get; set; }
}