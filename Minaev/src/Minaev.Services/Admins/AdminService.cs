using Minaev.Domain.Accounts;
using Minaev.Domain.Rents;
using Minaev.Domain.Services.Accounts;
using Minaev.Domain.Services.Admins;
using Minaev.Domain.Services.Rents;
using Minaev.Domain.Transports;
using Minaev.Services.Admins.Repositories;
using Minaev.Tools.Extensions;
using Minaev.Tools.Types;

namespace Minaev.Services.Admins;

public class AdminService : IAdminService
{
    private readonly IAdminRepository _adminRepository;
    private readonly IAccountsService _accountsService;
    private readonly IRentService _rentService;

    public AdminService(IAdminRepository adminRepository, IAccountsService accountsService, IRentService rentService)
    {
        _adminRepository = adminRepository;
        _accountsService = accountsService;
        _rentService = rentService;
    }

    public Result CreateAccount(AccountBlank blank, ID createdUserId)
    {
        PreprocessAccountBlank(blank);

        Account? existAccount = _accountsService.GetAccountByUserName(blank.Username);
        if (existAccount is not null) return Result.Fail("Аккаунт с таким username уже существует");

        Result validateAccountResult = ValidateAccount(blank);
        if (!validateAccountResult.IsSuccess) return validateAccountResult;

        String passwordHash = blank.Password.GetHash();
        Account newAccount = Account.Create(blank.Username, passwordHash, blank.IsAdmin, blank.Balance, createdUserId);

        _accountsService.SaveAccount(newAccount);
        return Result.Success();
    }

    private void PreprocessAccountBlank(AccountBlank blank)
    {
        blank.Username = blank.Username.Trim();
        blank.Password = blank.Password.Trim();
    }

    private Result ValidateAccount(AccountBlank blank)
    {
        if (blank.Username.IsNullOrWhitespace()) return Result.Fail("Не указан логин");
        if (blank.Password.IsNullOrWhitespace()) return Result.Fail("Не указан пароль");
        if (blank.Balance < 0) return Result.Fail("Баланс не может быть меньше 0");

        return Result.Success();
    }

    public Account[] GetAccounts(Int32 start, Int32 count)
    {
        return _adminRepository.GetAccounts(start, count);
    }

    public Result UpdateAccount(ID accountId, AccountBlank blank, ID adminId)
    {
        PreprocessAccountBlank(blank);

        Account? editedAccount = _accountsService.GetAccount(accountId);
        if (editedAccount is null) throw new Exception($"Не удалось найти редактируемого пользователя с id {accountId}");

        Account? existAccount = _accountsService.GetAccountByUserName(blank.Username);
        if (existAccount is not null) return Result.Fail("Аккаунт с таким username уже существует");

        Result validateAccountResult = ValidateAccount(blank);
        if (!validateAccountResult.IsSuccess) return validateAccountResult;

        String passwordHash = blank.Password.GetHash();
        editedAccount.Update(blank.Username, passwordHash, blank.IsAdmin, blank.Balance, adminId);

        _accountsService.SaveAccount(editedAccount);

        return Result.Success();
    }

    public Result DeleteAccount(ID accountId, ID adminId)
    {
        _adminRepository.DeleteAccount(accountId, adminId);

        return Result.Success();
    }

    #region  Transports

    public Transport[] GetTransports(Int32 start, Int32 count, String transportType)
    {
        return _adminRepository.GetTransports(start, count, transportType);
    }

    #endregion Transports

    #region  Rents

    public Result CreateRent(RentBlank blank, ID createdUserId)
    {
        Result validateResult = ValidateRentBlank(blank);
        if (!validateResult.IsSuccess) return validateResult;

        DateTime timeStart = ToDateTime(blank.TimeStart);
        DateTime? timeEnd = blank.TimeEnd is null ? null : ToDateTime(blank.TimeEnd);

        Rent createdRent = new Rent(ID.New(), blank.TransportId, blank.UserId, timeStart, timeEnd,
            blank.PriceOfUnit, blank.PriceType, blank.FinalPrice, createdUserId
        );

        _rentService.SaveRent(createdRent);
        return Result.Success();
    }

    private static DateTime ToDateTime(String dateTime)
    {
        return DateTime.Parse(dateTime, null, System.Globalization.DateTimeStyles.RoundtripKind);
    }

    public Result UpdateRent(ID rendId, RentBlank blank)
    {
        Rent? editedRent = _rentService.GetRent(rendId);
        if (editedRent is null) return Result.Fail("Не найдена редактируемая аренда");

        Result validateResult = ValidateRentBlank(blank);
        if (!validateResult.IsSuccess) return validateResult;

        DateTime timeStart = ToDateTime(blank.TimeStart).ToUniversalTime();
        DateTime? timeEnd = blank.TimeEnd is null ? null : ToDateTime(blank.TimeEnd).ToUniversalTime();

        editedRent.Update(
            blank.TransportId, blank.UserId, timeStart,
            timeEnd, blank.PriceOfUnit, blank.PriceType, blank.FinalPrice
        );

        _rentService.SaveRent(editedRent);

        return Result.Success();
    }

    private Result ValidateRentBlank(RentBlank blank)
    {
        if (blank.TimeStart.IsNullOrWhitespace()) return Result.Fail("Не указано время начала");
        if (blank.PriceOfUnit < 0) return Result.Fail("Цена не может быть ниже 0");
        if (blank.PriceType.IsNullOrWhitespace()) return Result.Fail("Не указан тип оплаты");

        return Result.Success();
    }

    public Result DeleteRent(ID rendId)
    {
        _adminRepository.DeleteRent(rendId);

        return Result.Success();
    }

    #endregion Rents

}