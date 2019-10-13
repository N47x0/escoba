using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class EntityConfiguration : IEntityTypeConfiguration<Data.RulesEntity>
{
    public void Configure(EntityTypeBuilder<Data.RulesEntity> builder)
    {
        builder
            .HasKey(e => e.Id);

        builder
            .Property(e => e.Id)
            .UseIdentityAlwaysColumn();
    }
}