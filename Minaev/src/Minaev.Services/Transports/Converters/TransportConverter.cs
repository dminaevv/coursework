using EFConfigurator.Models;
using Minaev.Domain.Transports;

namespace Minaev.Services.Transports.Converters;

public static class TransportConverter
{
    public static Transport ToTransport(this TransportDB db)
    {
        return new Transport(
            db.Id, db.Identifier, db.Description, db.TransportType, db.Model,
            db.Color, db.OwnerId, db.Latitude, db.Longitude, db.CanBeRented, db.MinutePrice,
            db.DayPrice
        );
    }

    public static TransportDB ToDb(this Transport transport)
    {
        return new TransportDB()
        {
            Id = transport.Id,
            Identifier = transport.Identifier,
            Description = transport.Description,
            TransportType = transport.TransportType,
            Model = transport.Model,
            Color = transport.Color,
            OwnerId = transport.OwnerId,
            Latitude = transport.Latitude,
            Longitude = transport.Longitude,
            CanBeRented = transport.CanBeRented,
            MinutePrice = transport.MinutePrice,
            DayPrice = transport.DayPrice
        };
    }
}
