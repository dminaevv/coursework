namespace Minaev.Domain.Transports;

public class TransportInfo
{
    public String Identifier { get; }
    public String? Description { get; }
    public String TransportType { get; }
    public String Model { get; }
    public String Color { get; }
    public String OwnerId { get; }
    public Double Latitude { get; }
    public Double Longitude { get; }
    public Boolean CanBeRented { get; }
    public Double? MinutePrice { get; }
    public Double? DayPrice { get; }

    public TransportInfo(
        String identifier, String? description, String transportType, String model,
        String color, String ownerId, Double latitude, Double longitude, Boolean canBeRented,
        Double? minutePrice, Double? dayPrice
        )
    {
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
