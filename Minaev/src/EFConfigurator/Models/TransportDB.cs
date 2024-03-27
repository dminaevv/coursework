namespace EFConfigurator.Models;

public class TransportDB
{
    public Int64 Id { get; set; }
    public String Identifier { get; set; }
    public String? Description { get; set; }
    public String TransportType { get; set; }
    public String Model { get; set; }
    public String Color { get; set; }
    public Int64 OwnerId { get; set; }
    public Double Latitude { get; set; }
    public Double Longitude { get; set; }
    public Boolean CanBeRented { get; set; }
    public Double? MinutePrice { get; set; }
    public Double? DayPrice { get; set; }
    public DateTime CreatedDateTimeUtc { get; set; }
    public DateTime? ModifiedDateTimeUtc { get; set; }
    public Boolean IsRemoved { get; set; }
}
