using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using TickerQ.EntityFrameworkCore.Configurations;
using TickerQ.Utilities.Models.Ticker;

namespace TickerQPlayground;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options) { }

    //protected override void OnModelCreating(ModelBuilder builder)
    //{
    //    base.OnModelCreating(builder);

    //    builder.ApplyConfiguration(new TimeTickerConfigurations());
    //    builder.ApplyConfiguration(new CronTickerConfigurations());
    //    builder.ApplyConfiguration(new CronTickerOccurrenceConfigurations());

    //}
}