namespace Intex_Group3_6.Models;

public class Order
{
    public required int transactionId { get; set; }
    public required int userId { get; set; }
    public required DateTime transactionDate { get; set; }
    public required string dayOfWeek { get; set; }
    public required int time { get; set; }
    public required string entryMode { get; set; }
    public required float transactionAmount { get; set; }
    public required string countryOfTrnsaction { get; set; }
    public required string shippingAddress { get; set; }
    public required string bank { get; set; }
    public required string typeOfCard { get; set; }
    public bool? fraud { get; set; }
}