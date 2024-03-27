using Microsoft.AspNetCore.Mvc;
using Minaev.Domain.Rents;
using Minaev.Domain.Services.Admins;
using Minaev.Domain.Services.Rents;
using Minaev.Tools.Types;
using Minaev.WebAPI.Infrastructure;

namespace Minaev.WebAPI.Controllers.Admin;

[Authorize("Admin")]
public class AdminRentController : BaseController
{
    private readonly IAdminService _adminService;
    private readonly IRentService _rentService;

    public AdminRentController(IAdminService adminService, IRentService rentService)
    {
        _adminService = adminService;
        _rentService = rentService;
    }

    /// <summary>
    /// Получение информации по аренде по id
    /// </summary>
    /// <param name="start"></param>
    /// <param name="count"></param>
    /// <param name="transportType"></param>
    /// <returns></returns>

    [HttpGet("/api/Admin/Rent/{rentId}")]
    public IActionResult GetRentInfo(String rentId)
    {
        DataResult<RentInfo?> getRentInfoResult = _rentService.GetRentInfo(rentId, SystemUser.Id);
        if (!getRentInfoResult.IsSuccess) return BadRequest(getRentInfoResult.ErrorsAsString);

        return Ok(getRentInfoResult.Data);
    }

    /// <summary>
    /// Получение истории аренд пользователя с id={userId}
    /// </summary>
    /// <returns></returns>
    [HttpGet("/api/Admin/UserHistory/{userId}")]
    public IActionResult GetUserRentHistory(String userId)
    {
        Rent[] clientRentHistory = _rentService.GetClientRentHistory(userId);

        return Ok(clientRentHistory);
    }

    /// <summary>
    /// Получение истории аренд транспорта с id={transportId}
    /// </summary>
    /// <param name="rentId"></param>
    /// <returns></returns>
    [HttpGet("/api/Admin/TransportHistory/{transportId}")]
    public IActionResult GetTransportRentHistory(String transportId)
    {
        DataResult<Rent[]> transportRentHistoryResult = _rentService.GetTransportRentHistory(transportId, SystemUser.Id);
        if (!transportRentHistoryResult.IsSuccess) return BadRequest(transportRentHistoryResult.ErrorsAsString);

        return Ok(transportRentHistoryResult.Data);
    }

    /// <summary>
    ///  Создание новой аренды
    /// </summary>
    /// <param name="transportId"></param>
    /// <returns></returns>

    [HttpPost("/api/Admin/Rent")]
    public IActionResult CreateRent([FromBody] RentBlank blank)
    {
        //Только администраторы
        Result createRentResult = _adminService.CreateRent(blank, SystemUser.Id);
        if (!createRentResult.IsSuccess) return BadRequest(createRentResult.ErrorsAsString);

        return Ok();
    }

    public record CompleteRentRequest(Double Lat, Double Long);

    /// <summary>
    /// Завершение аренды транспорта по id аренды
    /// </summary>
    /// <param name="blank"></param>
    /// <returns></returns>
    [HttpPost("/api/Admin/Rent/End/{rentId}")]
    public IActionResult CompleteTransportRent(String rentId, [FromBody] CompleteRentRequest request)
    {
        Result completeTransportRentResult = _rentService.CompletionTransportRent(rentId, request.Lat, request.Long, SystemUser.Id);
        if (!completeTransportRentResult.IsSuccess) return BadRequest(completeTransportRentResult.ErrorsAsString);

        return Ok();
    }

    /// <summary>
    /// Изменение записи об аренде по id
    /// </summary>
    /// <param name="blank"></param>
    /// <returns></returns>

    [HttpPut("/api/Admin/Rent/{id}")]
    public IActionResult UpdateRent(String id, [FromBody] RentBlank blank)
    {
        Result updateRentResult = _adminService.UpdateRent(id, blank);
        if (!updateRentResult.IsSuccess) return BadRequest(updateRentResult.ErrorsAsString);

        return Ok();
    }

    /// <summary>   
    /// Удаление информации об аренде по id
    /// </summary>
    /// <param name="rentId"></param>
    /// <returns></returns>
    [HttpDelete("/api/Admin/Rent/{rentId}")]
    public IActionResult DeleteRent(String rentId)
    {
        Result updateRentResult = _adminService.DeleteRent(rentId);
        if (!updateRentResult.IsSuccess) return BadRequest(updateRentResult.ErrorsAsString);

        return Ok();
    }
}