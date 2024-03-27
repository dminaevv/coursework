using Microsoft.AspNetCore.Mvc;
using Minaev.Domain.Accounts;
using Minaev.Domain.Services.Accounts;
using Minaev.Domain.Services.Admins;
using Minaev.Tools.Types;
using Minaev.WebAPI.Infrastructure;

namespace Minaev.WebAPI.Controllers.Admin;

[Authorize("Admin")]
public class AdminAccountController : BaseController
{
    private readonly IAdminService _adminService;
    private readonly IAccountsService _accountsService;

    public AdminAccountController(IAdminService adminService, IAccountsService accountsService)
    {
        _adminService = adminService;
        _accountsService = accountsService;
    }

    /// <summary>
    /// Получение списка всех аккаунтов
    /// </summary>
    /// <returns></returns>
    [HttpGet("/api/Admin/Account")]
    public IActionResult GetAccounts(Int32 start, Int32 count)
    {
        Account[] users = _adminService.GetAccounts(start, count);

        return Ok(users);
    }

    /// <summary>
    /// Получение информации об аккаунте по id
    /// </summary>
    /// <returns></returns>

    [HttpGet("/api/Admin/Account/{id}")]
    public IActionResult GetUserInfo(String id)
    {
        AccountInfo? userInfo = _accountsService.GetAccountInfo(id);
        return Ok(userInfo);
    }


    /// <summary>
    /// Создание администратором нового аккаунта
    /// </summary>
    /// <returns></returns>
    [HttpPost("/api/Admin/Account")]
    public IActionResult CreateAccount([FromBody] AccountBlank accountBlank)
    {
        Result createUserResult = _adminService.CreateAccount(accountBlank, SystemUser.Id);
        if (!createUserResult.IsSuccess) return BadRequest(createUserResult.ErrorsAsString);

        return Ok();
    }

    /// <summary>
    /// Изменение администратором аккаунта по id
    /// </summary>
    /// <param name="accountBlank"></param>
    /// <returns></returns>

    [HttpPut("/api/Admin/Account/{id}")]
    public IActionResult UpdateUser(String accountId, [FromBody] AccountBlank accountBlank)
    {
        Result updateUserResult = _adminService.UpdateAccount(accountId, accountBlank, SystemUser.Id);
        if (!updateUserResult.IsSuccess) return BadRequest(updateUserResult.ErrorsAsString);

        return Ok();
    }

    /// <summary>
    ///  Удаление аккаунта по id
    /// </summary>
    /// <param name="userBlank"></param>
    /// <returns></returns>
    [HttpDelete("/api/Admin/Account/{id}")]
    public IActionResult DeleteAccount(String id)
    {
        Result deleteUserResult = _adminService.DeleteAccount(id, SystemUser.Id);
        if (!deleteUserResult.IsSuccess) return BadRequest(deleteUserResult.ErrorsAsString);

        return Ok();
    }
}