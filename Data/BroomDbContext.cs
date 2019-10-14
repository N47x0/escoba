using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class BroomDbContext : DbContext {
    public DbSet<Data.RulesEntity> Rules { get; set; }
    public BroomDbContext(DbContextOptions<BroomDbContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Data.RulesEntity>().ToTable("rules");
        //modelBuilder.HasDefaultSchema("public");
        //modelBuilder.ApplyConfiguration(new EntityConfiguration());
    }
}