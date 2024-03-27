using Minaev.Domain.Rents;
using Minaev.Domain.Transports;
using Minaev.Tools.Types;

namespace Minaev.Services.Rents.Repositories;

public interface IRentRepository
{
    void SaveRent(Rent rent);
    Rent? GetRent(ID id);
    Transport[] GetAvailableForRentTransports(String type);
    Rent[] GetClientRentHistory(ID clientId);
    Rent[] GetTransportRentHistory(ID transportId);
}
