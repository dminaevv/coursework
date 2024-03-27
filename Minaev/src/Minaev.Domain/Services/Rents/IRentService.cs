using Minaev.Domain.Rents;
using Minaev.Domain.Transports;
using Minaev.Tools.Types;

namespace Minaev.Domain.Services.Rents;

public interface IRentService
{
    void SaveRent(Rent rent);
    Rent? GetRent(ID id);
    Transport[] GetAvailableForRentTransports(Double lat, Double @long, Double radius, String type);
    DataResult<RentInfo?> GetRentInfo(ID rentId, ID userId);
    Rent[] GetClientRentHistory(ID clientId);
    DataResult<Rent[]> GetTransportRentHistory(ID transportId, ID userId);
    Result RentTransport(ID transportId, String rentType, ID userId);
    Result CompletionTransportRent(ID rentId, Double lat, Double @long, ID userId);
}