using Minaev.Domain.Accounts;
using Minaev.Domain.Services.Accounts;
using Minaev.Domain.Services.Transports;
using Minaev.Domain.Transports;
using Minaev.Services.Transports.Repositories;
using Minaev.Tools.Extensions;
using Minaev.Tools.Types;

namespace Minaev.Services.Transports;

public class TransportService : ITransportService
{
    private readonly ITransportRepository _transportRepository;
    private readonly IAccountsService _accountsService;

    public TransportService(ITransportRepository transportRepository, IAccountsService accountsService)
    {
        _transportRepository = transportRepository;
        _accountsService = accountsService;
    }

    public Result AddTransport(TransportBlank blank)
    {
        ID transportId = ID.New();

        Result validateTransportResult = ValidateTransport(blank);
        if (!validateTransportResult.IsSuccess) return validateTransportResult;

        Transport validTransport = new(
            transportId, blank.Identifier, blank.Description, blank.TransportType, blank.Model,
            blank.Color, blank.OwnerId, blank.Latitude, blank.Longitude, blank.CanBeRented,
            blank.MinutePrice, blank.DayPrice
        );

        _transportRepository.SaveTransport(validTransport);

        return Result.Success();
    }

    public Transport? GetTransport(ID id)
    {
        return _transportRepository.GetTransport(id);
    }

    public TransportInfo? GetTransportInfo(ID id)
    {
        Transport? transport = GetTransport(id);
        if (transport is null) return null;

        return new TransportInfo(
            transport.Identifier, transport.Description, transport.TransportType,
            transport.Model, transport.Color, transport.OwnerId.Value.ToString(), transport.Latitude,
            transport.Longitude, transport.CanBeRented, transport.MinutePrice, transport.DayPrice
        );
    }

    public Result EditTransport(ID transportId, TransportBlank blank, ID userId)
    {
        Transport? transport = GetTransport(transportId);
        if (transport is null) return Result.Fail("Транспорт не найден");

        Account? account = _accountsService.GetAccount(userId);
        if (account is null) throw new Exception("Не найден авторизованный аккаунт");

        if (!account.IsAdmin && transport.OwnerId != userId)
            return Result.Fail("Редактировать транспорт может только владелец");

        Result validateTransportResult = ValidateTransport(blank);
        if (!validateTransportResult.IsSuccess) return validateTransportResult;

        Transport validTransport = new(
            transportId, blank.Identifier, blank.Description, blank.TransportType, blank.Model,
            blank.Color, blank.OwnerId, blank.Latitude, blank.Longitude, blank.CanBeRented,
            blank.MinutePrice, blank.DayPrice
        );

        _transportRepository.SaveTransport(validTransport);

        return Result.Success();
    }

    public void UpdateTransportPosition(ID transportId, Double lat, Double @long)
    {
        _transportRepository.UpdateTransportPosition(transportId, lat, @long);
    }

    private Result ValidateTransport(TransportBlank blank)
    {
        if (blank.CanBeRented)
        {
            if (blank.MinutePrice is null && blank.DayPrice is null) return Result.Fail("Не указана стоимость аренды");
        }

        if (blank.Identifier.IsNullOrWhitespace()) return Result.Fail("Не указан номерной знак");
        if (blank.TransportType.IsNullOrWhitespace()) return Result.Fail("Не указан тип транспорта");
        if (blank.Model.IsNullOrWhitespace()) return Result.Fail("Не указана модель транспорта");
        if (blank.Color.IsNullOrWhitespace()) return Result.Fail("Не указан цвет транспорта");

        return Result.Success();
    }

    public Result DeleteTransport(ID transportId, ID userId)
    {
        Transport? transport = GetTransport(transportId);
        if (transport is null) return Result.Fail("Транспорт не найден");

        Account? account = _accountsService.GetAccount(userId);
        if (account is null) throw new Exception("Не найден авторизованный аккаунт");

        if (!account.IsAdmin && transport.OwnerId != userId)
            return Result.Fail("Редактировать транспорт может только владелец");

        _transportRepository.DeleteTransport(transportId, userId);

        return Result.Success();
    }
}