using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Intex_Group3_6.Models;

public class Order : IEnumerable
{
    [Key]
    public required int transactionId { get; set; }
    public required int userId { get; set; }
    public DateTime transactionDate { get; set; }
    public string? dayOfWeek { get; set; }
    public int? time { get; set; }
    public string? entryMode { get; set; }
    public float? transactionAmount { get; set; }
    public string? typeOfTransaction { get; set; }
    public string? countryOfTransaction { get; set; }
    public string? shippingAddress { get; set; }
    public string? bank { get; set; }
    public string? typeOfCard { get; set; }
    public string? fraud { get; set; }
    public IEnumerator GetEnumerator()
    {
        throw new NotImplementedException();
    }
}