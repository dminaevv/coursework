using Minaev.Tools.Types;

namespace Minaev.Services.Payments.Repositories;

public interface IPaymentRepository
{
    void IncreaseBalance(ID accountId, Double increaseSum);
}
