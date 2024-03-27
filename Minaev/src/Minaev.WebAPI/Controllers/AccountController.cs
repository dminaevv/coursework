using Microsoft.AspNetCore.Mvc;
using Minaev.Domain.Accounts;
using Minaev.Domain.Services.Accounts;
using Minaev.Tools.Types;
using Minaev.WebAPI.Infrastructure;
using Minaev.WebAPI.Providers;
using System.Security.Claims;

namespace Minaev.WebAPI.Controllers;

public class AccountController : BaseController
{
    private readonly IAccountsService _accountService;
    public AccountController(IAccountsService accountService)
    {
        _accountService = accountService;
    }

    /// <summary>
    /// получение данных о текущем аккаунте
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpGet("/api/Account/Me")]
    public IActionResult GetInfo()
    {
        AccountInfo? usrInfo = _accountService.GetAccountInfo(SystemUser.Id);
        if (usrInfo is null) return BadRequest("Не удалось найти пользователя");

        return Ok(usrInfo);
    }

    public record UserRequest(String Username, String Password);

    /// <summary>
    /// получение нового jwt токена пользователя
    /// </summary>
    /// <param name="userRequest"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("/api/Account/SignIn")]
    public IActionResult SignIn([FromBody] UserRequest userRequest)
    {
        Account? account = _accountService.SignIn(userRequest.Username, userRequest.Password);
        if (account is null) return BadRequest("Неправильный логин или пароль");

        String jwtToken = Jwt.Create(new Claim[]
        {
            new(ClaimTypes.Name, account.Username),
            new(ClaimNames.IsAdmin, account.IsAdmin.ToString()),
            new(ClaimNames.Id, account.Id.Value.ToString()),
        });

        return Ok(jwtToken);
    }
    /// <summary>
    ///  регистрация нового аккаунта
    /// </summary>
    /// <param name="userRequest"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("/api/Account/SignUp")]
    public IActionResult SignUp([FromBody] UserRequest userRequest)
    {
        Result signUpResult = _accountService.SignUp(userRequest.Username, userRequest.Password);
        if (!signUpResult.IsSuccess) return BadRequest(signUpResult.ErrorsAsString);

        return Ok();
    }

    /// <summary>
    /// выход из аккаунт
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpPost("/api/Account/SignOut")]
    public IActionResult SignOut()
    {
        _accountService.BanJwtToken(SystemUser.Token);

        return Ok();
    }

    /// <summary>
    ///  обновление своего аккаунта
    /// </summary>
    /// <param name="userRequest"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPut("/api/Account/Update")]
    public IActionResult Update([FromBody] UserRequest userRequest)
    {
        Result updateResult = _accountService.Update(SystemUser.Id, userRequest.Username, userRequest.Password);
        if (!updateResult.IsSuccess) return BadRequest(updateResult.ErrorsAsString);

        return Ok();
    }
}