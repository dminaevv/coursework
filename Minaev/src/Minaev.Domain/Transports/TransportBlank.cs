namespace Minaev.Domain.Transports;

public class TransportBlank
{
    public Int64 OwnerId { get; set; }
    public String Identifier { get; set; }
    public String? Description { get; set; }
    public String TransportType { get; set; }
    public String Model { get; set; }
    public String Color { get; set; }
    public Double Latitude { get; set; }
    public Double Longitude { get; set; }
    public Boolean CanBeRented { get; set; }
    public Double? MinutePrice { get; set; }
    public Double? DayPrice { get; set; }

}
