namespace Minaev.Domain.Rents;

public class RentBlank
{
    public Int64 TransportId { get; set; }
    public Int64 UserId { get; set; }
    public String TimeStart { get; set; }
    public String? TimeEnd { get; set; }
    public Double PriceOfUnit { get; set; }
    public String PriceType { get; set; }
    public Double? FinalPrice { get; set; }
}