using Microsoft.AspNetCore.Mvc;
using Minaev.Domain.Services.Payments;
using Minaev.Tools.Types;
using Minaev.WebAPI.Infrastructure;

namespace Minaev.WebAPI.Controllers;

public class PaymentController : BaseController
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    /// <summary>
    ///  Добавляет на баланс аккаунта с id={accountId} 250 000 денежных единиц
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpPost("/api/Payment/Hesoyam/{accountId}")]
    public IActionResult IncreaseBalance(String accountId)
    {
        Result increaseBalanceResult = _paymentService.IncreaseBalance(accountId, SystemUser.Id);
        if (!increaseBalanceResult.IsSuccess) return BadRequest(increaseBalanceResult.ErrorsAsString);

        return Ok();
    }
}