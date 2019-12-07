using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace game_server.Scaffolds
{
    public partial class Game_Server_DBContext : DbContext
    {
        public Game_Server_DBContext()
        {
        }

        public Game_Server_DBContext(DbContextOptions<Game_Server_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<GameInfo> GameInfo { get; set; }
        public virtual DbSet<GameSessions> GameSessions { get; set; }
        public virtual DbSet<GameStatistics> GameStatistics { get; set; }
        public virtual DbSet<Rules> Rules { get; set; }
        public virtual DbSet<UserGameSessions> UserGameSessions { get; set; }
        public virtual DbSet<UserStatistic> UserStatistic { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Data Source=SORFACE;Initial Catalog=Game_Server_DB;Trusted_Connection=True;MultipleActiveResultSets=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameInfo>(entity =>
            {
                entity.ToTable("GameInfo", "Games");

                entity.Property(e => e.GameInfoId).ValueGeneratedNever();
            });

            modelBuilder.Entity<GameSessions>(entity =>
            {
                entity.HasKey(e => e.GameSessionId);

                entity.ToTable("GameSessions", "Games");

                entity.HasIndex(e => e.GameInfoId);

                entity.HasIndex(e => e.GameStatisticId)
                    .IsUnique();

                entity.Property(e => e.GameSessionId).ValueGeneratedNever();

                entity.HasOne(d => d.GameInfo)
                    .WithMany(p => p.GameSessions)
                    .HasForeignKey(d => d.GameInfoId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.GameStatistic)
                    .WithOne(p => p.GameSessions)
                    .HasForeignKey<GameSessions>(d => d.GameStatisticId);
            });

            modelBuilder.Entity<GameStatistics>(entity =>
            {
                entity.HasKey(e => e.GameStatisticId);

                entity.ToTable("GameStatistics", "Games");

                entity.HasIndex(e => e.GameInfoId);

                entity.HasIndex(e => e.GameSessionId)
                    .IsUnique();

                entity.Property(e => e.GameStatisticId).ValueGeneratedNever();

                entity.HasOne(d => d.GameInfo)
                    .WithMany(p => p.GameStatistics)
                    .HasForeignKey(d => d.GameInfoId);
            });

            modelBuilder.Entity<Rules>(entity =>
            {
                entity.HasKey(e => e.RuleId);

                entity.ToTable("Rules", "Games");

                entity.HasIndex(e => e.GameInfoId);

                entity.Property(e => e.RuleId).ValueGeneratedNever();

                entity.HasOne(d => d.GameInfo)
                    .WithMany(p => p.Rules)
                    .HasForeignKey(d => d.GameInfoId);
            });

            modelBuilder.Entity<UserGameSessions>(entity =>
            {
                entity.HasKey(e => e.UserGameSessionId);

                entity.ToTable("UserGameSessions", "Games");

                entity.HasIndex(e => e.GameInfoId);

                entity.HasIndex(e => e.GameSessionId);

                entity.HasIndex(e => e.GameStatisticId);

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserGameSessionId).ValueGeneratedNever();

                entity.HasOne(d => d.GameInfo)
                    .WithMany(p => p.UserGameSessions)
                    .HasForeignKey(d => d.GameInfoId);

                entity.HasOne(d => d.GameSession)
                    .WithMany(p => p.UserGameSessions)
                    .HasForeignKey(d => d.GameSessionId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.GameStatistic)
                    .WithMany(p => p.UserGameSessions)
                    .HasForeignKey(d => d.GameStatisticId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserGameSessions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<UserStatistic>(entity =>
            {
                entity.HasIndex(e => e.GameInfoId);

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserStatisticId).ValueGeneratedNever();

                entity.HasOne(d => d.GameInfo)
                    .WithMany(p => p.UserStatistic)
                    .HasForeignKey(d => d.GameInfoId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserStatistic)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("Users", "Games");

                entity.Property(e => e.UserId).ValueGeneratedNever();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
