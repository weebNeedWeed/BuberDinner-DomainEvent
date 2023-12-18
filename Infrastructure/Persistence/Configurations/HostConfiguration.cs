using Domain.Host;
using Domain.Host.ValueObjects;
using Domain.Menu.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;
public class HostConfiguration : IEntityTypeConfiguration<Host>
{
    public void Configure(EntityTypeBuilder<Host> builder)
    {
        ConfigureHostsTable(builder);
        ConfigureHostMenuIdsTable(builder);
    }

    private void ConfigureHostMenuIdsTable(EntityTypeBuilder<Host> builder)
    {
        builder.OwnsMany(x => x.MenuIds, mb =>
        {
            mb.WithOwner().HasForeignKey("HostId");

            mb.HasKey("Id");

            mb.Property(x => x.Value)
                .HasColumnName("HostMenuId");
        });

        builder.Metadata.FindNavigation(nameof(Host.MenuIds))
            !.SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureHostsTable(EntityTypeBuilder<Host> builder)
    {
        builder.ToTable("Hosts");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => HostId.Create(value));
    }
}
