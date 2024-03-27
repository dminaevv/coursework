using Minaev.Domain.Transports;
using Minaev.Tools.Types;

namespace Minaev.Domain.Services.Transports;

public interface ITransportService
{
    Result AddTransport(TransportBlank blank);
    Transport? GetTransport(ID id);
    TransportInfo? GetTransportInfo(ID id);
    Result EditTransport(ID transportId, TransportBlank blank, ID userId);
    void UpdateTransportPosition(ID transportId, Double lat, Double @long);
    Result DeleteTransport(ID transportId, ID userId);
}