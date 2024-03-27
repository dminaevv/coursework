using Minaev.Tools.Types;

namespace Minaev.Domain.Rents;

public class RentInfo
{
    public ID Id { get; }
    public ID TransportId { get; }
    public ID UserId { get; }
    public DateTime TimeStart { get; }
    public DateTime? TimeEnd { get; }
    public Double PriceOfUnit { get; }
    public String PriceType { get; }
    public Double? FinalPrice { get; }

    public RentInfo(
        ID id, ID transportId, ID userId, DateTime timeStart, DateTime? timeEnd,
        Double priceOfUnit, String priceType, Double? finalPrice
    )
    {
        Id = id;
        TransportId = transportId;
        UserId = userId;
        TimeStart = timeStart;
        TimeEnd = timeEnd;
        PriceOfUnit = priceOfUnit;
        PriceType = priceType;
        FinalPrice = finalPrice;
    }
}