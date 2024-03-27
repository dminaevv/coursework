    using Microsoft.AspNetCore.Mvc;
using Minaev.Domain.Rents;
using Minaev.Domain.Services.Rents;
using Minaev.Domain.Transports;
using Minaev.Tools.Types;
using Minaev.WebAPI.Infrastructure;

namespace Minaev.WebAPI.Controllers;

public class RentController : BaseController
{
    private readonly IRentService _rentService;

    public RentController(IRentService rentService)
    {
        _rentService = rentService;
    }

    /// <summary>
    /// Получение транспорта доступного для аренды по параметрам
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet("/api/Rent/Transport")]
    public IActionResult GetAvailableForRentTransport(Double lat, Double @long, Double radius, String type)//Тип [Car, Bike, Scooter, All]
    {
        Transport[] availableTransports = _rentService.GetAvailableForRentTransports(lat, @long, radius, type);

        return Ok(availableTransports);
    }

    /// <summary>
    /// Получение информации об аренде по id
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpGet("/api/Rent/{rentId}")]
    public IActionResult GetRentInfo(String rentId)
    {
        DataResult<RentInfo?> rentInfoResult = _rentService.GetRentInfo(rentId, SystemUser.Id);
        if (!rentInfoResult.IsSuccess) return BadRequest(rentInfoResult.ErrorsAsString);

        return Ok(rentInfoResult.Data);
    }

    /// <summary>
    /// Получение истории аренд текущего аккаунта
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpGet("/api/Rent/MyHistory")]
    public IActionResult GetClientRentHistory()
    {
        Rent[] clientRentHistory = _rentService.GetClientRentHistory(SystemUser.Id);

        return Ok(clientRentHistory);
    }

    /// <summary>
    /// Получение истории аренд транспорта
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpGet("/api/Rent/TransportHistory/{transportId}")]
    public IActionResult GetTransportRentHistory(String transportId)
    {
        DataResult<Rent[]> transportRentHistoryResult = _rentService.GetTransportRentHistory(transportId, SystemUser.Id);
        if (!transportRentHistoryResult.IsSuccess) return BadRequest(transportRentHistoryResult.ErrorsAsString);

        return Ok(transportRentHistoryResult.Data);
    }

    /// <summary>
    ///  Аренда транспорта в личное пользование
    /// </summary>
    /// <param name="transportId"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPost("/api/Rent/New/{transportId}")]
    public IActionResult RentTransport(String transportId, [FromBody] String rentType)
    {
        Result? rentTransportResult = _rentService.RentTransport(transportId, rentType, SystemUser.Id);
        if (!rentTransportResult.IsSuccess) return BadRequest(rentTransportResult.ErrorsAsString);

        return Ok();
    }

    public record TransportRentCompletionRequest(Double Lat, Double Long);
    /// <summary>
    /// Завершение аренды транспорта по id аренды
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpPost("/api/Rent/End/{rentId}")]
    public IActionResult CompletionTransportRent(String rentId, [FromBody] TransportRentCompletionRequest request)
    {
        Result completionTransportRentResult = _rentService.CompletionTransportRent(rentId, request.Lat, request.Long, SystemUser.Id);
        if (!completionTransportRentResult.IsSuccess) return BadRequest(completionTransportRentResult.ErrorsAsString);

        return Ok();
    }
}