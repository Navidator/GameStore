﻿using GameStore.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.DataBase
{
    public class GameStoreContext : DbContext
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
        public DbSet<RoleModel> Roles { get; set; }
        public DbSet<UserModel> Users { get; set; }
    }
}
