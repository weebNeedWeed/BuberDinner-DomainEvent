using Domain.Host.ValueObjects;
using Domain.Menu;
using Domain.Menu.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;
internal class MenuConfiguration : IEntityTypeConfiguration<Menu>
{
    public void Configure(EntityTypeBuilder<Menu> builder)
    {
        ConfigureMenusTable(builder);
        ConfigureMenuSectionsTable(builder);
        ConfigureMenuDinnerIdsTable(builder);
        ConfigureMenuReviewIdsTable(builder);
    }

    private void ConfigureMenuReviewIdsTable(EntityTypeBuilder<Menu> builder)
    {
        builder.OwnsMany(x => x.DinnerIds, dib =>
        {
            dib.ToTable("MenuDinnerIds");

            dib.WithOwner().HasForeignKey("MenuId");

            dib.HasKey("Id");

            dib.Property("Id").ValueGeneratedOnAdd();

            dib.Property(x => x.Value)
                .HasColumnName("DinnerId")
                .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(Menu.DinnerIds))!.
            SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureMenuDinnerIdsTable(EntityTypeBuilder<Menu> builder)
    {
        builder.OwnsMany(x => x.MenuReviewIds, dib =>
        {
            dib.ToTable("MenuReviewIds");

            dib.WithOwner().HasForeignKey("MenuId");

            dib.HasKey("Id");

            dib.Property("Id").ValueGeneratedOnAdd();

            dib.Property(x => x.Value)
                .HasColumnName("MenuReviewIds")
                .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(Menu.MenuReviewIds))!.
            SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureMenusTable(EntityTypeBuilder<Menu> builder)
    {
        builder.ToTable("Menus");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => MenuId.Create(value));

        builder.OwnsOne(x => x.AverageRating);

        builder.Property(x => x.HostId)
            .HasConversion(
                id => id.Value,
                value => HostId.Create(value));

        builder.Property(x => x.Name)
                .HasMaxLength(100);

        builder.Property(x => x.Description)
                .HasMaxLength(100);
    }

    private void ConfigureMenuSectionsTable(EntityTypeBuilder<Menu> builder)
    {
        builder.OwnsMany(x => x.Sections, sb =>
        {
            sb.ToTable("MenuSections");

            sb.WithOwner().HasForeignKey("MenuId");

            sb.HasKey("Id", "MenuId");

            sb.Property(x => x.Id)
                .ValueGeneratedNever()
                .HasColumnName("MenuSectionId")
                .HasConversion(
                    id => id.Value,
                    value => MenuSectionId.Create(value));

            sb.Property(x => x.Name)
                .HasMaxLength(100);

            sb.Property(x => x.Description)
                .HasMaxLength(100);

            sb.OwnsMany(x => x.Items, ib =>
            {
                ib.ToTable("MenuItems");

                ib.WithOwner().HasForeignKey("MenuSectionId", "MenuId");

                ib.HasKey("Id", "MenuSectionId", "MenuId");

                ib.Property(x => x.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("MenuItemId")
                    .HasConversion(
                        id => id.Value,
                        value => MenuItemId.Create(value));

                ib.Property(x => x.Name)
                 .HasMaxLength(100);

                ib.Property(x => x.Description)
                    .HasMaxLength(100);
            });

            sb.Navigation(s => s.Items).Metadata.SetField("_items");
            sb.Navigation(s => s.Items)
                .Metadata.SetPropertyAccessMode(PropertyAccessMode.Field);
        });

        builder.Metadata.FindNavigation(nameof(Menu.Sections))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
