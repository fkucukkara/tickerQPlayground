using Microsoft.EntityFrameworkCore;
using TickerQ.Dashboard.DependencyInjection;
using TickerQ.DependencyInjection;
using TickerQ.EntityFrameworkCore.DependencyInjection;
using TickerQ.Utilities.Base;
using TickerQPlayground;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddTickerQ(options =>
{
    options.SetMaxConcurrency(4); // Optional
    //options.SetExceptionHandler<MyExceptionHandler>(); // Optional
    options.AddOperationalStore<MyDbContext>(efOpt =>
    {
        efOpt.UseModelCustomizerForMigrations();
        efOpt.CancelMissedTickersOnApplicationRestart();
    });
    options.AddDashboard(basePath: "/tickerq-dashboard");
    options.AddDashboardBasicAuth();
});

var app = builder.Build();

app.UseTickerQ();
app.UseHttpsRedirection();

app.MapGet("/hello", () =>
{
    return TypedResults.Ok();
});


app.Run();
