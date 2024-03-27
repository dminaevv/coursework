using Minaev.Domain.Accounts;
using Minaev.Domain.Services.Accounts;
using Minaev.Domain.Services.Payments;
using Minaev.Services.Payments.Repositories;
using Minaev.Tools.Types;

namespace Minaev.Services.Payments;

public class PaymentService : IPaymentService
{
    private readonly IAccountsService _accountsService;
    private readonly IPaymentRepository _paymentRepository;

    public PaymentService(IAccountsService accountsService, IPaymentRepository paymentRepository)
    {
        _accountsService = accountsService;
        _paymentRepository = paymentRepository;
    }

    public Result IncreaseBalance(ID accountId, ID userId)
    {
        Account? account = _accountsService.GetAccount(userId);
        if (account is null) throw new Exception("Не найден аккаунт авторизованного пользователя");

        if (!account.IsAdmin && accountId != userId) return Result.Fail("Нет доступа");
        Double increaseSum = 250_000;

        _paymentRepository.IncreaseBalance(accountId, increaseSum);

        return Result.Success();
    }
}