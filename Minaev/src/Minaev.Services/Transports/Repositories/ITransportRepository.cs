using Minaev.Domain.Transports;
using Minaev.Tools.Types;

namespace Minaev.Services.Transports.Repositories;

public interface ITransportRepository
{
    void SaveTransport(Transport transport);
    Transport? GetTransport(ID id);
    void UpdateTransportPosition(ID transportId, Double lat, Double @long);
    void DeleteTransport(ID transportId, ID userId);
}
