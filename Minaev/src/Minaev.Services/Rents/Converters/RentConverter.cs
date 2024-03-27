using EFConfigurator.Models;
using Minaev.Domain.Rents;

namespace Minaev.Services.Rents.Converters;

public static class RentConverter
{
    public static Rent ToRent(this RentDB db)
    {
        return new Rent(
            db.Id, db.TransportId, db.UserId, db.TimeStartUtc,
            db.TimeEndUtc, db.PriceOfUnit, db.PriceType, db.FinalPrice, db.CreatedUserId
        );
    }

    public static RentDB ToDb(this Rent rent)
    {
        return new RentDB()
        {
            Id = rent.Id,
            TransportId = rent.TransportId,
            UserId = rent.UserId,
            TimeStartUtc = rent.TimeStartUtc,
            TimeEndUtc = rent.TimeEndUtc,
            PriceOfUnit = rent.PriceOfUnit,
            PriceType = rent.PriceType,
            FinalPrice = rent.FinalPrice,
            CreatedUserId = rent.CreatedUserId
        };
    }
}
