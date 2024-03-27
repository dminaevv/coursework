using Microsoft.AspNetCore.Mvc;
using Minaev.Domain.Services.Transports;
using Minaev.Domain.Transports;
using Minaev.Tools.Types;
using Minaev.WebAPI.Infrastructure;

namespace Minaev.WebAPI.Controllers;

public class TransportController : BaseController
{
    private readonly ITransportService _transportService;

    public TransportController(ITransportService transportService)
    {
        _transportService = transportService;
    }

    /// <summary>
    ///  Получение информации о транспорте по id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("/api/Transport/{id}")]
    public IActionResult GetTransportInfo(String id)
    {
        TransportInfo? transportInfo = _transportService.GetTransportInfo(id);
        if (transportInfo is null) return NotFound();

        return Ok(transportInfo);
    }

    /// <summary>
    /// Добавление нового транспорта
    /// </summary>
    /// <param name="blank"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPost("/api/Transport")]
    public IActionResult AddTransport([FromBody] TransportBlank blank)
    {
        blank.OwnerId = SystemUser.Id;
        Result addTransportResult = _transportService.AddTransport(blank);
        if (!addTransportResult.IsSuccess) return BadRequest(addTransportResult.ErrorsAsString);

        return Ok();
    }

    /// <summary>
    ///  Изменение транспорта оп id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="blank"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPut("/api/Transport/{id}")]
    public IActionResult EditTransport(String id, [FromBody] TransportBlank blank)
    {
        blank.OwnerId = SystemUser.Id;
        Result editTransportResult = _transportService.EditTransport(id, blank, SystemUser.Id);
        if (!editTransportResult.IsSuccess) return BadRequest(editTransportResult.ErrorsAsString);

        return Ok();
    }

    /// <summary>
    /// Удаление транспорта по id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize]
    [HttpDelete("/api/Transport/{id}")]
    public IActionResult DeleteTransport(String id)
    {
        Result addTransportResult = _transportService.DeleteTransport(id, SystemUser.Id);
        if (!addTransportResult.IsSuccess) return BadRequest(addTransportResult.ErrorsAsString);

        return Ok();
    }
}