using Minaev.Tools.Types;

namespace Minaev.Domain.Services.Payments;

public interface IPaymentService
{
    Result IncreaseBalance(ID accountId, ID userId);
}