using Minaev.Tools.Types;

namespace Minaev.Domain.Transports;

public class Transport
{
    public ID Id { get; set; }
    public String Identifier { get; }
    public String? Description { get; }
    public String TransportType { get; }
    public String Model { get; }
    public String Color { get; }
    public ID OwnerId { get; }
    public Double Latitude { get; }
    public Double Longitude { get; }
    public Boolean CanBeRented { get; }
    public Double? MinutePrice { get; }
    public Double? DayPrice { get; }

    public Transport(
        ID id,
        String identifier, String? description, String transportType, String model,
        String color, ID ownerId, Double latitude, Double longitude, Boolean canBeRented,
        Double? minutePrice, Double? dayPrice
        )
    {
        Id = id;
        Identifier = identifier;
        Description = description;
        TransportType = transportType;
        Model = model;
        Color = color;
        OwnerId = ownerId;
        Latitude = latitude;
        Longitude = longitude;
        CanBeRented = canBeRented;
        MinutePrice = minutePrice;
        DayPrice = dayPrice;
    }
}
