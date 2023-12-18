using Domain.Common.Models;
using Domain.Host;
using Domain.Menu;
using Infrastructure.Persistence.Configurations;
using Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistence;
public class BuberDinnerDbContext : DbContext
{
    private readonly PublishDomainEventsInterceptor _publishDomainEventsInterceptor;

    public BuberDinnerDbContext(
        DbContextOptions<BuberDinnerDbContext> options,
        PublishDomainEventsInterceptor publishDomainEventsInterceptor) 
        : base(options)
    {
        _publishDomainEventsInterceptor = publishDomainEventsInterceptor;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Ignore<List<IDomainEvent>>()
            .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_publishDomainEventsInterceptor);
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<Menu> Menus { get; set; }
    public DbSet<Host> Hosts { get; set; }
}
