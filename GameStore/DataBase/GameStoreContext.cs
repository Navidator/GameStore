using GameStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameStore.DataBase
{
    public class GameStoreContext : IdentityDbContext<UserModel/*, UserRoleModel, int*/>
    {
        public GameStoreContext(DbContextOptions<GameStoreContext> options) : base(options)
        {
        }

        public DbSet<OrderedGamesModel> OrderedGames { get; set; }
        public DbSet<CommentModel> Comments { get; set; }
        public DbSet<CurrencyModel> Currency { get; set; }
        public DbSet<GameModel> Games { get; set; }
        public DbSet<GenderModel> Gender { get; set; }
        public DbSet<GenreModel> Genres { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<PaymentTypeModel> PaymentTypes { get; set; }
        public DbSet<GamesAndGenresModel> GamesAndGenres { get; set; }
        public DbSet<RefreshTokenModel> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GenreModel>()
                .HasMany(e => e.Children)
                .WithOne(e => e.Parent)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GamesAndGenresModel>()
                .HasOne(x => x.Game)
                .WithMany(x => x.GameAndGenre)
                .HasForeignKey(x => x.GameId);
            modelBuilder.Entity<GamesAndGenresModel>()
                .HasOne(x => x.Genre)
                .WithMany(x => x.GameAndGenre)
                .HasForeignKey(x => x.GenreId);
        }
    }
}
