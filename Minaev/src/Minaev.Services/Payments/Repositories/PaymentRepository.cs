using EFConfigurator.Models;
using EFConfigurator.Repositories;
using Minaev.Tools.Types;

namespace Minaev.Services.Payments.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly IDbRepository _dbRepository;

    public PaymentRepository(IDbRepository dbRepository)
    {
        _dbRepository = dbRepository;
    }

    public void IncreaseBalance(ID accountId, Double increaseSum)
    {
        AccountDB editedAccount = _dbRepository.First<AccountDB>(a => a.Id == accountId.Value);
        editedAccount.Balance += increaseSum;

        _dbRepository.SaveChanges();
    }

}
