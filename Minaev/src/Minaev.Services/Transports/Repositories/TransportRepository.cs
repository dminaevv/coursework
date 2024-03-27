using EFConfigurator.Models;
using EFConfigurator.Repositories;
using Minaev.Domain.Transports;
using Minaev.Services.Transports.Converters;
using Minaev.Tools.Types;

namespace Minaev.Services.Transports.Repositories;

public class TransportRepository : ITransportRepository
{
    private readonly IDbRepository _dbRepository;

    public TransportRepository(IDbRepository dbRepository)
    {
        _dbRepository = dbRepository;
    }

    public void SaveTransport(Transport transport)
    {
        TransportDB? existTransport = _dbRepository.FirstOrDefault<TransportDB>(u => u.Id == transport.Id.Value);
        if (existTransport is null)
        {
            TransportDB db = transport.ToDb();
            db.CreatedDateTimeUtc = DateTime.UtcNow;

            _dbRepository.Add(db);
        }
        else
        {
            existTransport.Identifier = transport.Identifier;
            existTransport.Description = transport.Description;
            existTransport.TransportType = transport.TransportType;
            existTransport.Model = transport.Model;
            existTransport.Color = transport.Color;
            existTransport.OwnerId = transport.OwnerId;
            existTransport.Latitude = transport.Latitude;
            existTransport.Longitude = transport.Longitude;
            existTransport.CanBeRented = transport.CanBeRented;
            existTransport.MinutePrice = transport.MinutePrice;
            existTransport.DayPrice = transport.DayPrice;
            existTransport.ModifiedDateTimeUtc = DateTime.UtcNow;
        }
        _dbRepository.SaveChanges();
    }

    public Transport? GetTransport(ID id)
    {
        return _dbRepository.FirstOrDefault<TransportDB>(t => t.Id == id.Value && t.IsRemoved == false)?
            .ToTransport();
    }

    public void UpdateTransportPosition(ID transportId, Double lat, Double @long)
    {
        TransportDB updatedTransport = _dbRepository.First<TransportDB>(t => t.Id == transportId.Value);
        updatedTransport.Latitude = lat;
        updatedTransport.Longitude = lat;

        _dbRepository.SaveChanges();
    }

    public void DeleteTransport(ID transportId, ID userId)
    {
        TransportDB deletedTransport = _dbRepository.First<TransportDB>(u => u.Id == transportId.Value);
        deletedTransport.IsRemoved = true;
        deletedTransport.ModifiedDateTimeUtc = DateTime.UtcNow;

        _dbRepository.SaveChanges();
    }

}
