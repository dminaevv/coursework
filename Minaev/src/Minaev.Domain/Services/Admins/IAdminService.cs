using Minaev.Domain.Accounts;
using Minaev.Domain.Rents;
using Minaev.Domain.Transports;
using Minaev.Tools.Types;

namespace Minaev.Domain.Services.Admins;

public interface IAdminService
{
    #region Accounts

    Result CreateAccount(AccountBlank blank, ID createdUserId);
    Account[] GetAccounts(Int32 start, Int32 count);
    Result UpdateAccount(ID accountId, AccountBlank blank, ID adminId);
    Result DeleteAccount(ID accountId, ID adminId);

    #endregion Accounts

    #region Transports

    Transport[] GetTransports(Int32 start, Int32 count, String transportType);

    #endregion Transports

    #region  Rents

    Result CreateRent(RentBlank blank, ID createdUserId);
    Result UpdateRent(ID rendId, RentBlank blank);
    Result DeleteRent(ID rendId);

    #endregion Rents
}