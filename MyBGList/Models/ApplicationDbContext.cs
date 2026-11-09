using Microsoft.EntityFrameworkCore;
using MyBGList.Models;

namespace MyBGList.Models
{
    public class ApplicationDbContext : DbContext
    {
        // Added DBSets
        public DbSet<BoardGame> BoardGames => Set<BoardGame>();
        public DbSet<Domain> Domains => Set<Domain>();
        public DbSet<Mechanic> Mechanics => Set<Mechanic>();
        public DbSet<BoardGames_Domains> BoardGames_Domains
        => Set<BoardGames_Domains>();
        public DbSet<BoardGames_Mechanics> BoardGames_Mechanics => Set<BoardGames_Mechanics>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Publisher> Publishers => Set<Publisher>();
        public DbSet<BoardGames_Categories> BoardGames_Categories => Set<BoardGames_Categories>();

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // TODO: custom code here

            modelBuilder.Entity<BoardGames_Domains>()
              .HasKey(i => new { i.BoardGameId, i.DomainId });

            modelBuilder.Entity<BoardGames_Domains>()
              .HasOne(x => x.BoardGame)
              .WithMany(y => y.BoardGames_Domains)
              .HasForeignKey(f => f.BoardGameId)
              .IsRequired()
              .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BoardGames_Domains>()
              .HasOne(o => o.Domain)
              .WithMany(m => m.BoardGames_Domains)
              .HasForeignKey(f => f.DomainId)
              .IsRequired()
              .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BoardGames_Mechanics>()
            .HasKey(i => new { i.BoardGameId, i.MechanicId });

            modelBuilder.Entity<BoardGames_Categories>()
              .HasKey(i => new { i.BoardGameId, i.CategoryId });

            modelBuilder.Entity<BoardGames_Categories>()
              .HasOne(x => x.BoardGame)
              .WithMany(y => y.BoardGames_Categories)
              .HasForeignKey(f => f.BoardGameId)
              .IsRequired()
              .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BoardGames_Categories>()
              .HasOne(o => o.Category)
              .WithMany(m => m.BoardGames_Categories)
              .HasForeignKey(f => f.CategoryId)
              .IsRequired()
              .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BoardGame>()
                .HasOne(b => b.Publisher)
                .WithMany(p => p.BoardGames)
                .HasForeignKey(b => b.PublisherId)
                .OnDelete(DeleteBehavior.SetNull);

        }

    }
}