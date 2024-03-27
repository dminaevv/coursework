using EFConfigurator.Models;
using EFConfigurator.Repositories;
using Minaev.Domain.Accounts;
using Minaev.Domain.Transports;
using Minaev.Services.Accounts.Converters;
using Minaev.Services.Transports.Converters;
using Minaev.Tools.Types;

namespace Minaev.Services.Admins.Repositories;

public class AdminRepository : IAdminRepository
{
    private readonly IDbRepository _dbRepository;

    public AdminRepository(IDbRepository dbRepository)
    {
        _dbRepository = dbRepository;
    }

    public Account[] GetAccounts(Int32 start, Int32 count)
    {
        return _dbRepository.Get<AccountDB>(a => a.IsRemoved == false)
            .Skip(start)
            .Take(count)
            .Select(db => db.ToAccount())
            .ToArray();
    }

    public void DeleteAccount(ID accountId, ID adminId)
    {
        AccountDB deletedAccount = _dbRepository.First<AccountDB>(a => a.Id == accountId.Value);
        deletedAccount.IsRemoved = true;
        deletedAccount.ModifiedDateTimeUtc = DateTime.UtcNow;
        deletedAccount.ModifiedUserId = adminId.Value;

        _dbRepository.SaveChanges();
    }

    #region Transports

    public Transport[] GetTransports(Int32 start, Int32 count, String transportType)
    {
        return _dbRepository.Get<TransportDB>(t => t.IsRemoved == false)
            .Skip(start)
            .Take(count)
            .Select(db => db.ToTransport())
            .ToArray();
    }

    #endregion Transports


    #region  Rents

    public void DeleteRent(ID rendId)
    {
        RentDB deletedRent = _dbRepository.First<RentDB>(a => a.Id == rendId.Value);
        deletedRent.IsRemoved = true;
        deletedRent.ModifiedDateTimeUtc = DateTime.UtcNow;

        _dbRepository.SaveChanges();
    }

    #endregion Rents

}
