using EFConfigurator.Models;
using Minaev.Domain.Accounts;

namespace Minaev.Services.Accounts.Converters;

public static class UsersConverter
{
    public static Account ToAccount(this AccountDB db)
    {
        return new Account(
            db.Id, db.Username, db.Password, db.IsAdmin, db.Balance, db.CreatedDateTimeUtc,
            db.CreatedUserId, db.ModifiedDateTimeUtc, db.ModifiedUserId, db.IsRemoved
        );
    }

    public static AccountDB ToDb(this Account account)
    {
        return new AccountDB(
            account.Id, account.Username, account.Password, account.IsAdmin,
            account.Balance, account.CreatedDateTimeUtc, account.CreatedUserId, account.ModifiedDateTimeUtc,
            account.ModifiedUserId, account.IsRemoved
        );
    }

    public static JwtToken ToToken(this JwtTokenDB db)
    {
        return new JwtToken(
            db.Token
        );
    }
}
