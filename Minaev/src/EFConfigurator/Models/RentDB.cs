namespace EFConfigurator.Models;

public class RentDB
{
    public Int64 Id { get; set; }
    public Int64 TransportId { get; set; }
    public Int64 UserId { get; set; }
    public DateTime TimeStartUtc { get; set; }
    public DateTime? TimeEndUtc { get; set; }
    public Double PriceOfUnit { get; set; }
    public String PriceType { get; set; }
    public Double? FinalPrice { get; set; }
    public DateTime CreatedDateTimeUtc { get; set; }
    public Int64 CreatedUserId { get; set; }
    public DateTime? ModifiedDateTimeUtc { get; set; }
    public Boolean IsRemoved { get; set; }
}

