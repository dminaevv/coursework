using Minaev.Domain.Accounts;
using Minaev.Domain.Transports;
using Minaev.Tools.Types;

namespace Minaev.Services.Admins.Repositories;

public interface IAdminRepository
{
    #region Accounts

    Account[] GetAccounts(Int32 start, Int32 count);
    void DeleteAccount(ID accountId, ID adminId);

    #endregion Accounts

    #region Transports

    Transport[] GetTransports(Int32 start, Int32 count, String transportType);

    #endregion Transports

    #region Rents

    void DeleteRent(ID rendId);

    #endregion Rents
}
