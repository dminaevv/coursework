using Minaev.Domain.Accounts;
using Minaev.Domain.Rents;
using Minaev.Domain.Services.Accounts;
using Minaev.Domain.Services.Rents;
using Minaev.Domain.Services.Transports;
using Minaev.Domain.Transports;
using Minaev.Services.Rents.Repositories;
using Minaev.Tools.Extensions;
using Minaev.Tools.Types;

namespace Minaev.Services.Rents;

public class RentService : IRentService
{
    private readonly IRentRepository _rentRepository;
    private readonly ITransportService _transportService;
    private readonly IAccountsService _accountsService;

    public RentService(IRentRepository rentRepository, ITransportService transportService, IAccountsService accountsService)
    {
        _rentRepository = rentRepository;
        _transportService = transportService;
        _accountsService = accountsService;
    }

    public void SaveRent(Rent rent)
    {
        _rentRepository.SaveRent(rent);
    }

    public Rent? GetRent(ID id)
    {
        return _rentRepository.GetRent(id);
    }

    public Transport[] GetAvailableForRentTransports(Double lat, Double @long, Double radius, String type)
    {
        Transport[] availableForRentTransports = _rentRepository.GetAvailableForRentTransports(type);

        return availableForRentTransports.Where(transport =>
                CalculateDistance(lat, @long, transport.Latitude, transport.Longitude) <= radius)
            .ToArray();
    }

    private Double CalculateDistance(Double lat1, Double lon1, Double lat2, Double lon2)
    {
        Double earthRadius = 6378.1;

        Double dLat = ToRadians(lat2 - lat1);
        Double dLon = ToRadians(lon2 - lon1);

        lat1 = ToRadians(lat1);
        lat2 = ToRadians(lat2);

        Double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                   Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);

        Double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        Double distance = earthRadius * c;

        return distance;
    }

    private Double ToRadians(Double degrees)
    {
        return degrees * Math.PI / 180.0;
    }

    public DataResult<RentInfo?> GetRentInfo(ID rentId, ID userId)
    {
        Rent? rent = GetRent(rentId);
        if (rent == null) return DataResult<RentInfo?>.Fail($"Аренда с id {rentId} не найдена");

        Transport? rentTransport = _transportService.GetTransport(rent.TransportId);
        if (rentTransport is null) throw new Exception($"У аренты {rentId} не найден арендованный транпорт");

        Account? account = _accountsService.GetAccount(userId);
        if (account is null) throw new Exception("Не найден авторизованный аккаунт");

        if (!account.IsAdmin && rent.UserId != userId && rentTransport.OwnerId != userId)
            return DataResult<RentInfo?>.Fail("Нет доступа");

        RentInfo rentInfo = new(
            rent.Id, rent.TransportId, rent.UserId, rent.TimeStartUtc.ToLocalTime(),
            rent.TimeEndUtc?.ToLocalTime(), rent.PriceOfUnit, rent.PriceType, rent.FinalPrice
        );

        return DataResult<RentInfo?>.Success(rentInfo);
    }

    public Rent[] GetClientRentHistory(ID clientId)
    {
        return _rentRepository.GetClientRentHistory(clientId);
    }

    public DataResult<Rent[]> GetTransportRentHistory(ID transportId, ID userId)
    {
        Transport? transport = _transportService.GetTransport(transportId);
        if (transport is null) return DataResult<Rent[]>.Success(new Rent[0]);

        Account? account = _accountsService.GetAccount(userId);
        if (account is null) throw new Exception("Не найден авторизованный аккаунт");

        if (!account.IsAdmin && transport.OwnerId != userId) return DataResult<Rent[]>.Fail("Нет доступа");

        Rent[] transportRentHistory = _rentRepository.GetTransportRentHistory(transportId);

        return DataResult<Rent[]>.Success(transportRentHistory);
    }

    public Result RentTransport(ID transportId, String rentType, ID userId)
    {
        Transport? transport = _transportService.GetTransport(transportId);
        if (transport is null) return Result.Fail("Транспорт не найден");

        if (transport.OwnerId == userId) return Result.Fail("Нельзя арендовать собственный транспорт");

        String[] acceptableRentType = { "Minutes", "Days" };
        if (!rentType.OneOf(acceptableRentType)) return Result.Fail("Указан недопустимый тип аренды");

        Double priceOfUnit = rentType == "Minutes"
            ? transport.MinutePrice ?? (transport.DayPrice!.Value / 24) / 60
            : transport.DayPrice ?? transport.MinutePrice!.Value * 1440;

        Rent newRent = Rent.New(transportId, userId, priceOfUnit, rentType, createdUserId: userId);
        SaveRent(newRent);

        return Result.Success();
    }

    public Result CompletionTransportRent(ID rentId, Double lat, Double @long, ID userId)
    {
        Rent? rent = GetRent(rentId);
        if (rent is null) return Result.Fail("Аренда не найдена");

        Account? account = _accountsService.GetAccount(userId);
        if (account is null) throw new Exception("Не найден авторизованный аккаунт");

        if (!account.IsAdmin && rent.CreatedUserId != userId) return Result.Fail("Нет доступа");

        DateTime completionDateTimeUtc = DateTime.UtcNow;
        Double finalPrice = CalculateFinalPrice(rent, completionDateTimeUtc);

        rent.Complete(completionDateTimeUtc, finalPrice);

        _rentRepository.SaveRent(rent);
        _transportService.UpdateTransportPosition(rent.TransportId, lat, @long);

        return Result.Success();
    }

    private Double CalculateFinalPrice(Rent rent, DateTime completionDateTimeUtc)
    {
        Double difference = rent.PriceType == "Minutes"
            ? (completionDateTimeUtc - rent.TimeStartUtc).TotalMinutes
            : (completionDateTimeUtc - rent.TimeStartUtc).TotalDays;

        return difference * rent.PriceOfUnit;
    }
}