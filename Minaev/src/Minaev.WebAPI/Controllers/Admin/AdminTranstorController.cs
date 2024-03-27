using Microsoft.AspNetCore.Mvc;
using Minaev.Domain.Services.Admins;
using Minaev.Domain.Services.Transports;
using Minaev.Domain.Transports;
using Minaev.Tools.Types;
using Minaev.WebAPI.Infrastructure;

namespace Minaev.WebAPI.Controllers.Admin;

[Authorize("Admin")]
public class AdminTranstorController : BaseController
{
    private readonly IAdminService _adminService;
    private readonly ITransportService _transportService;

    public AdminTranstorController(IAdminService adminService, ITransportService transportService)
    {
        _adminService = adminService;
        _transportService = transportService;
    }

    /// <summary>
    /// Получение списка всех транспортных средств
    /// </summary>
    /// <param name="start"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    [HttpGet("/api/Admin/Transport")]
    public IActionResult GetTransports(Int32 start, Int32 count, String transportType) //[Car, Bike, Scooter, All]
    {
        Transport[] transports = _adminService.GetTransports(start, count, transportType);

        return Ok(transports);
    }

    /// <summary>
    /// Получение информации о транспортном средстве по id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("/api/Admin/Transport/{id}")]
    public IActionResult GetTransportInfo(String id)
    {
        TransportInfo? transportInfo = _transportService.GetTransportInfo(id);
        return Ok(transportInfo);
    }

    /// <summary>
    /// Создание нового транспортного средства
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost("/api/Admin/Transport")]
    public IActionResult CreateTransport([FromBody] TransportBlank blank)
    {
        Result createTransportResult = _transportService.AddTransport(blank);
        if (!createTransportResult.IsSuccess) return BadRequest(createTransportResult.ErrorsAsString);

        return Ok();
    }

    /// <summary>
    /// Изменение транспортного средства по id
    /// </summary>
    /// <param name="blank"></param>
    /// <returns></returns>

    [HttpPut("/api/Admin/Transport/{id}")]
    public IActionResult UpdateTransport(String id, [FromBody] TransportBlank blank)
    {
        Result updateTransportResult = _transportService.EditTransport(id, blank, SystemUser.Id);
        if (!updateTransportResult.IsSuccess) return BadRequest(updateTransportResult.ErrorsAsString);

        return Ok();
    }

    [HttpDelete("/api/Admin/Transport/{id}")]
    public IActionResult DeleteTransport(String id)
    {
        Result deleteTransportResult = _transportService.DeleteTransport(id, SystemUser.Id);
        if (!deleteTransportResult.IsSuccess) return BadRequest(deleteTransportResult.ErrorsAsString);

        return Ok();
    }
}