using Minaev.Tools.Types;
using Minaev.WebAPI;
using Minaev.WebAPI.Middleware;
using Minaev.WebAPI.Providers;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Initialize(services =>
{
    services.AddJwtAuthentication();
    services.AddJwtAuthorization();
    services.AddHttps();
    services.AddResponseCompressionProviders();
    services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new IDConverter());
        });
    services.AddControllers();
    services.AddSwaggerGen();
});

WebApplication app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseRouting();
app.UseMiddleware<JWTMiddleware>();
app.UseAuthorization();
app.UseHttps();
app.UseResponseCompression();
app.UseStaticFiles();
app.UseCors();
app.UseDefaultEndpoints();
app.Run();