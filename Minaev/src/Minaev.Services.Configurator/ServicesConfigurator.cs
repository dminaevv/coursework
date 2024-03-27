using EFConfigurator.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Minaev.Domain.Services.Accounts;
using Minaev.Domain.Services.Admins;
using Minaev.Domain.Services.Payments;
using Minaev.Domain.Services.Rents;
using Minaev.Domain.Services.Transports;
using Minaev.Services.Accounts;
using Minaev.Services.Accounts.Repositories;
using Minaev.Services.Admins;
using Minaev.Services.Admins.Repositories;
using Minaev.Services.Payments;
using Minaev.Services.Payments.Repositories;
using Minaev.Services.Rents;
using Minaev.Services.Rents.Repositories;
using Minaev.Services.Transports;
using Minaev.Services.Transports.Repositories;

namespace Minaev.Services.Configurator;

public static class ServicesConfigurator
{
    public static void Initialize(this IServiceCollection services)
    {
        using ServiceProvider serviceProvider = services.BuildServiceProvider();

        services.AddScoped<IDbRepository, DbRepository>();

        #region Repositories

        services.AddScoped<IAccountsRepository, AccountsRepository>();
        services.AddScoped<IAdminRepository, AdminRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IRentRepository, RentRepository>();
        services.AddScoped<ITransportRepository, TransportRepository>();

        #endregion Repositories

        #region Services

        services.AddScoped<IAccountsService, AccountsService>();
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IRentService, RentService>();
        services.AddScoped<ITransportService, TransportService>();

        #endregion Services
    }
}