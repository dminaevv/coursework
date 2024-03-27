using EFConfigurator.Models;
using EFConfigurator.Repositories;
using Minaev.Domain.Rents;
using Minaev.Domain.Transports;
using Minaev.Services.Rents.Converters;
using Minaev.Services.Transports.Converters;
using Minaev.Tools.Types;

namespace Minaev.Services.Rents.Repositories;

public class RentRepository : IRentRepository
{
    private readonly IDbRepository _dbRepository;

    public RentRepository(IDbRepository dbRepository)
    {
        _dbRepository = dbRepository;
    }

    public void SaveRent(Rent rent)
    {
        RentDB rentDb = rent.ToDb();

        RentDB? existRent = _dbRepository.FirstOrDefault<RentDB>(r => r.Id == rent.Id.Value);
        if (existRent is null)
        {
            rentDb.CreatedDateTimeUtc = DateTime.UtcNow;
            rentDb.IsRemoved = false;

            _dbRepository.Add(rentDb);
        }
        else
        {
            existRent.TransportId = rent.TransportId.Value;
            existRent.UserId = rent.UserId.Value;
            existRent.TimeStartUtc = rent.TimeStartUtc;
            existRent.TimeEndUtc = rent.TimeEndUtc;
            existRent.PriceOfUnit = rent.PriceOfUnit;
            existRent.PriceType = rent.PriceType;
            existRent.FinalPrice = rent.FinalPrice;
            existRent.CreatedUserId = rent.CreatedUserId.Value;
            existRent.ModifiedDateTimeUtc = DateTime.UtcNow;
        }

        _dbRepository.SaveChanges();
    }

    public Rent? GetRent(ID id)
    {
        return _dbRepository.FirstOrDefault<RentDB>(t => t.Id == id.Value && t.IsRemoved == false)
            ?.ToRent();
    }

    public Transport[] GetAvailableForRentTransports(String type)
    {
        return _dbRepository
            .Get<TransportDB>(
                t => t.TransportType == type
                     && t.CanBeRented == true
                     && t.IsRemoved == false
            )
            .Select(db => db.ToTransport())
            .ToArray();
    }

    public Rent[] GetClientRentHistory(ID clientId)
    {
        return _dbRepository.Get<RentDB>(r => r.UserId == clientId.Value)
            .Select(db => db.ToRent())
            .ToArray();
    }

    public Rent[] GetTransportRentHistory(ID transportId)
    {
        return _dbRepository.Get<RentDB>(r => r.TransportId == transportId.Value)
            .Select(db => db.ToRent())
            .ToArray();
    }
}
