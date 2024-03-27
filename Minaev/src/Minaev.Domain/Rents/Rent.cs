using Minaev.Tools.Types;

namespace Minaev.Domain.Rents;

public class Rent
{
    public ID Id { get; }
    public ID TransportId { get; private set; }
    public ID UserId { get; private set; }
    public DateTime TimeStartUtc { get; private set; }
    public DateTime? TimeEndUtc { get; private set; }
    public Double PriceOfUnit { get; private set; }
    public String PriceType { get; private set; }
    public Double? FinalPrice { get; private set; }
    public ID CreatedUserId { get; }

    public Rent(
        ID id, ID transportId, ID userId, DateTime timeStartUtc, DateTime? timeEndUtc,
        Double priceOfUnit, String priceType, Double? finalPrice, ID createdUserId)
    {
        Id = id;
        TransportId = transportId;
        UserId = userId;
        TimeStartUtc = timeStartUtc;
        TimeEndUtc = timeEndUtc;
        PriceOfUnit = priceOfUnit;
        PriceType = priceType;
        FinalPrice = finalPrice;
        CreatedUserId = createdUserId;
    }

    public static Rent New(ID transportId, ID userId, Double priceOfUnit, String priceType, ID createdUserId)
    {
        return new Rent(
            ID.New(), transportId, userId, timeStartUtc: DateTime.UtcNow,
            timeEndUtc: null, priceOfUnit, priceType, finalPrice: null, createdUserId
        );
    }

    public void Complete(DateTime timeEndUtc, Double finalPrice)
    {
        TimeEndUtc = timeEndUtc;
        FinalPrice = finalPrice;
    }

    public void Update(
       ID transportId, ID userId, DateTime timeStartUtc, DateTime? timeEndUtc,
        Double priceOfUnit, String priceType, Double? finalPrice)
    {
        TransportId = transportId;
        UserId = userId;
        TimeStartUtc = timeStartUtc;
        TimeEndUtc = timeEndUtc;
        PriceOfUnit = priceOfUnit;
        PriceType = priceType;
        FinalPrice = finalPrice;
    }
}